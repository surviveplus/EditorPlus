using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OfficeThemeSample
{
    public enum AccentColors
    {
        Outlook,
        Word,
        Excel,
        PowerPoint,
    } // end enum

    public static class OfficeAccentColor
    {

        public static void Apply(this ResourceDictionary me, AccentColors accent)
        {
            if (me == null) throw new ArgumentNullException("me");

            var a = (OfficeAccentColor.Palette.ContainsKey(accent) ? accent : AccentColors.PowerPoint);
            foreach (var kvp in OfficeAccentColor.Palette[a])
            {
                me[kvp.Key] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(kvp.Value));
            } // next kvp

        } // end function


        public static Dictionary<AccentColors, Dictionary<string, string>> Palette { get; private set; } = new Dictionary<AccentColors, Dictionary<string, string>>()
        {
            { AccentColors.Outlook, new Dictionary<string, string>(){
                { "Accent-Black-IsMouseOver-Background","#1D41B8" },

            } },

            { AccentColors.PowerPoint, new Dictionary<string, string>(){
                { "Accent-Black-IsMouseOver-Background","#b83b1d" },
                { "Accent-Black-IsMouseOver-BorderBrush","#d24726" },
                { "Accent-Black-IsPressed-Background","#d24726" },
                { "Accent-Black-IsPressed-BorderBrush","#ff5630" },
                { "Accent-Black-IsCheckedMoserOver-Background","#E95B39" },
                { "Accent-Black-IsCheckedMoserOver-BorderBrush","#ff5630" },
                { "Accent-Black-IsCheckedPressed-Background","#D4401D" },
                { "Accent-Black-IsCheckedPre4ssed-BorderBrush","#ff5630" },
                { "Accent-White-IsDefaulted-BorderBrush","#b7472a" },
                { "Accent-White-IsMouseOver-Background","#fce4dc" },
                { "Accent-WHite-IsMouseOver-BorderBrush","#f5ba9d" },
                { "Accent-White-IsPressed-Background","#f5ba9d" },
                { "Accent-White-IsPressed-BorderBrush","#dc5939" },
                { "Accent-White-IsCheckedMoserOver-Background","#F4C4AE" },
                { "Accent-White-IsCheckedMoserOver-BorderBrush","#DE6345" },
                { "Accent-White-IsCheckedPressed-Background","#F7A67E" },
                { "Accent-White-IsCheckedPre4ssed-BorderBrush","#CE5537" },
            } },

        }; // end property
    } // end class
} // end namespace
