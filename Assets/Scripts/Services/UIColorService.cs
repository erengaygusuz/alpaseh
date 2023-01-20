using FTRGames.Alpaseh.Model;
using System.Collections.Generic;
using UnityEngine;

namespace FTRGames.Alpaseh.Core
{
    public class UIColorService
    {
        private List<ColorScheme> ColorSchemes { get; set; }

        public bool IsColorSchemeChanged { get; set; }

        public void GetAllSchemes()
        {
            ColorSchemes = new List<ColorScheme>();

            ColorSchemes.Add(new ColorScheme
            {
                TextColor = new Color32(20, 46, 64, 255),
                BarBackgroundColor = new Color32(18, 125, 199, 255),
                ContentBackgroundColor = new Color32(135, 206, 255, 255),
                ButtonBackgroundColor = new Color32(73, 139, 184, 255)
            });

            ColorSchemes.Add(new ColorScheme
            {
                TextColor = new Color32(58, 61, 37, 255),
                BarBackgroundColor = new Color32(177, 194, 54, 255),
                ContentBackgroundColor = new Color32(221, 235, 122, 255),
                ButtonBackgroundColor = new Color32(145, 163, 15, 255)
            });

            ColorSchemes.Add(new ColorScheme
            {
                TextColor = new Color32(10, 66, 9, 255),
                BarBackgroundColor = new Color32(50, 204, 47, 255),
                ContentBackgroundColor = new Color32(172, 247, 171, 255),
                ButtonBackgroundColor = new Color32(88, 161, 87, 255)
            });

            ColorSchemes.Add(new ColorScheme
            {
                TextColor = new Color32(66, 11, 19, 255),
                BarBackgroundColor = new Color32(240, 89, 110, 255),
                ContentBackgroundColor = new Color32(219, 154, 163, 255),
                ButtonBackgroundColor = new Color32(176, 72, 87, 255)
            });
        }

        public ColorScheme GetActiveColorScheme
        {
            get
            {
                return ColorSchemes[PlayerPrefs.GetInt("Alpaseh-SelectedColorSchemeIndex", 0)];
            }
        }
    }
}