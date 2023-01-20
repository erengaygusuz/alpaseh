using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FTRGames.Alpaseh.Core
{
    public class ColorAssigner : MonoBehaviour
    {
        public enum ObjectType
        {
            TEXT = 0,
            BAR = 1,
            CONTENT = 2,
            BUTTON = 3
        }

        private List<string> ObjectTypes
        {
            get
            {
                return new List<string>()
                {
                    "TEXT",
                    "BAR",
                    "CONTENT",
                    "BUTTON"
                };
            }
        }

        public ObjectType SelectedObjectType;

        private UIColorService uiColorService;

        public bool IsColorSchemeChanged;

        [Inject]
        public void Construct(UIColorService uiColorService)
        {
            this.uiColorService = uiColorService;
        }

        private void Start()
        {
            AssignObject();
        }

        private void Update()
        {
            if (IsColorSchemeChanged)
            {
                AssignObject();

                IsColorSchemeChanged = false;
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

                    GetComponent<Image>().color = uiColorService.GetActiveColorScheme.ButtonBackgroundColor;

                    break;
                default:

                    GetComponent<Image>().color = uiColorService.GetActiveColorScheme.ButtonBackgroundColor;

                    break;
            }
        }
    }
}