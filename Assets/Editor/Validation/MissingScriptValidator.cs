using UnityEditor;
using UnityEngine;

namespace FTRGames.Alpaseh.Editor.Validation
{
    public static class MissingScriptValidator
    {
        [MenuItem("Alpaseh/Validation/Find Missing Scripts in Loaded Objects")]
        public static void FindMissingScriptsInLoadedObjects()
        {
            var objectsWithMissingScripts = 0;
            var missingScriptCount = 0;
            var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (var gameObject in gameObjects)
            {
                if (!ShouldInspect(gameObject))
                {
                    continue;
                }

                var count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject);
                if (count == 0)
                {
                    continue;
                }

                objectsWithMissingScripts++;
                missingScriptCount += count;

                Debug.LogWarning(
                    $"[Alpaseh Validation] Missing script x{count}: {GetLocation(gameObject)}",
                    gameObject);
            }

            if (missingScriptCount == 0)
            {
                Debug.Log("[Alpaseh Validation] No missing scripts found in loaded scene objects or loaded project prefabs.");
                return;
            }

            Debug.LogWarning(
                $"[Alpaseh Validation] Found {missingScriptCount} missing script component(s) on " +
                $"{objectsWithMissingScripts} object(s). Click the warnings above to select the affected objects.");
        }

        private static bool ShouldInspect(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return false;
            }

            var assetPath = AssetDatabase.GetAssetPath(gameObject);
            var isProjectAsset = !string.IsNullOrEmpty(assetPath) && assetPath.StartsWith("Assets/");
            var isSceneObject = gameObject.scene.IsValid();

            return isProjectAsset || isSceneObject;
        }

        private static string GetLocation(GameObject gameObject)
        {
            var assetPath = AssetDatabase.GetAssetPath(gameObject);
            if (!string.IsNullOrEmpty(assetPath))
            {
                return $"{assetPath} :: {GetHierarchyPath(gameObject.transform)}";
            }

            var sceneName = gameObject.scene.IsValid() ? gameObject.scene.name : "Unknown Scene";
            return $"{sceneName} :: {GetHierarchyPath(gameObject.transform)}";
        }

        private static string GetHierarchyPath(Transform transform)
        {
            var path = transform.name;

            while (transform.parent != null)
            {
                transform = transform.parent;
                path = $"{transform.name}/{path}";
            }

            return path;
        }
    }
}
