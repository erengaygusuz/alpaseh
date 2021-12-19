using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public abstract class ColorAssign
{
    public Color32 color;

    public Color32 GetColor 
    { 
        get 
        { 
            return color; 
        } 
    }
}

public class TextColorAssign : ColorAssign
{
    public TextColorAssign()
    {
        color = UIColorManager.Instance.GetActiveColorScheme.TextColor;
    }
}

public class BarBackgroundColorAssign : ColorAssign
{
    public BarBackgroundColorAssign()
    {
        color = UIColorManager.Instance.GetActiveColorScheme.BarBackgroundColor;
    }
}

public class ContentBackgroundColorAssign : ColorAssign
{
    public ContentBackgroundColorAssign()
    {
        color = UIColorManager.Instance.GetActiveColorScheme.ContentBackgroundColor;
    }
}

public class ButtonBackgroundAssign : ColorAssign
{
    public ButtonBackgroundAssign()
    {
        color = UIColorManager.Instance.GetActiveColorScheme.ButtonBackgroundColor;
    }
}