using FTRGames.Alpaseh.Enums;
using FTRGames.Alpaseh.Models;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FTRGames.Alpaseh.Services
{
    public sealed class ColorAssigner : MonoBehaviour
    {
        public ObjectType SelectedObjectType;

        private UIColorService uiColorService;

        [Inject]
        public void Construct(UIColorService uiColorService)
        {
            this.uiColorService = uiColorService;
        }

        private void Start()
        {
            EnsureColorService();
            uiColorService.ColorSchemeChanged += AssignObject;
            AssignObject();
        }

        private void OnDestroy()
        {
            if (uiColorService != null)
            {
                uiColorService.ColorSchemeChanged -= AssignObject;
            }
        }

        private void EnsureColorService()
        {
            if (uiColorService != null)
            {
                return;
            }

            // Some scene/prefab UI objects can run before VContainer injects this helper.
            // The service is stateless apart from PlayerPrefs, so a local fallback safely
            // preserves the active theme color and prevents high-score UI crashes.
            uiColorService = new UIColorService();
        }

        private void AssignObject()
        {
            EnsureColorService();

            ColorScheme colorScheme = uiColorService.GetActiveColorScheme;

            switch (SelectedObjectType)
            {
                case ObjectType.BAR:
                    AssignImageColor(colorScheme.BarBackgroundColor);
                    break;

                case ObjectType.TEXT:
                    AssignTextColor(colorScheme.TextColor);
                    break;

                case ObjectType.CONTENT:
                    AssignImageColor(colorScheme.ContentBackgroundColor);
                    break;

                case ObjectType.BUTTON:
                default:
                    AssignImageColor(colorScheme.ButtonBackgroundColor);
                    break;
            }
        }

        private void AssignImageColor(Color color)
        {
            if (TryGetComponent(out Image image))
            {
                image.color = color;
            }
        }

        private void AssignTextColor(Color color)
        {
            if (TryGetComponent(out Text text))
            {
                text.color = color;
            }
        }
    }
}
