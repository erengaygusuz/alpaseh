using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

namespace FTRGames.Alpaseh.Editor
{
    public static class InputSystemSceneMigrator
    {
        private const string SceneRoot = "Assets/Scenes";
        private const string MenuPath = "Tools/Alpaseh/Migrate UI EventSystems to Input System";

        [MenuItem(MenuPath)]
        public static void Migrate()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                Debug.LogWarning("Exit Play Mode before migrating EventSystems.");
                return;
            }

            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                return;
            }

            string previouslyOpenScenePath = SceneManager.GetActiveScene().path;
            string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { SceneRoot });

            int migratedSceneCount = 0;
            int migratedModuleCount = 0;

            try
            {
                foreach (string sceneGuid in sceneGuids)
                {
                    string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
                    Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

                    StandaloneInputModule[] legacyModules = scene
                        .GetRootGameObjects()
                        .SelectMany(root => root.GetComponentsInChildren<StandaloneInputModule>(true))
                        .ToArray();

                    if (legacyModules.Length == 0)
                    {
                        continue;
                    }

                    foreach (StandaloneInputModule legacyModule in legacyModules)
                    {
                        GameObject eventSystemObject = legacyModule.gameObject;
                        InputSystemUIInputModule inputSystemModule =
                            eventSystemObject.GetComponent<InputSystemUIInputModule>();

                        if (inputSystemModule == null)
                        {
                            inputSystemModule = eventSystemObject.AddComponent<InputSystemUIInputModule>();
                            inputSystemModule.AssignDefaultActions();
                            EditorUtility.SetDirty(inputSystemModule);
                        }

                        Object.DestroyImmediate(legacyModule, true);
                        migratedModuleCount++;
                    }

                    EditorSceneManager.MarkSceneDirty(scene);
                    EditorSceneManager.SaveScene(scene);
                    migratedSceneCount++;
                }
            }
            finally
            {
                if (!string.IsNullOrEmpty(previouslyOpenScenePath))
                {
                    EditorSceneManager.OpenScene(previouslyOpenScenePath, OpenSceneMode.Single);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            Debug.Log(
                $"Input System UI migration complete. Migrated {migratedModuleCount} input module(s) across {migratedSceneCount} scene(s).");
        }
    }
}
