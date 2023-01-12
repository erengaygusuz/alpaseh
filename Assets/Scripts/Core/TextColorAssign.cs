using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTRGames.Alpaseh.Core
{
    public class TextColorAssign : ColorAssign
    {
        public TextColorAssign()
        {
            color = UIColorManager.GetActiveColorScheme.TextColor;
        }
    }
}
