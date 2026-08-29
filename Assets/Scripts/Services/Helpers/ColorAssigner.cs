using FTRGames.Alpaseh.Enums;
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

        private void AssignObject()
        {
            switch (SelectedObjectType)
            {
                case ObjectType.BAR:
                    GetComponent<Image>().color = uiColorService.GetActiveColorScheme.BarBackgroundColor;
                    break;

                case ObjectType.TEXT:
                    GetComponent<Text>().color = uiColorService.GetActiveColorScheme.TextColor;
                    break;

                case ObjectType.CONTENT:
                    GetComponent<Image>().color = uiColorService.GetActiveColorScheme.ContentBackgroundColor;
                    break;

                case ObjectType.BUTTON:
                default:
                    GetComponent<Image>().color = uiColorService.GetActiveColorScheme.ButtonBackgroundColor;
                    break;
            }
        }
    }
}
