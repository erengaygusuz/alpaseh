using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private ColorAssign ColorAssignObject;

        public bool IsColorSchemeChanged;

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

                    ColorAssignObject = new BarBackgroundColorAssign();

                    GetComponent<Image>().color = ColorAssignObject.GetColor;

                    break;
                case ObjectType.TEXT:

                    ColorAssignObject = new TextColorAssign();

                    GetComponent<Text>().color = ColorAssignObject.GetColor;

                    break;
                case ObjectType.CONTENT:

                    ColorAssignObject = new ContentBackgroundColorAssign();

                    GetComponent<Image>().color = ColorAssignObject.GetColor;

                    break;
                case ObjectType.BUTTON:

                    ColorAssignObject = new ButtonBackgroundAssign();

                    GetComponent<Image>().color = ColorAssignObject.GetColor;

                    break;
                default:

                    ColorAssignObject = new ButtonBackgroundAssign();

                    GetComponent<Image>().color = ColorAssignObject.GetColor;

                    break;
            }
        }
    }
}