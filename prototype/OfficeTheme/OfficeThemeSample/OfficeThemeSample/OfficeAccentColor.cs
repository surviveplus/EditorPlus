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
        Project,
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
                { "Accent-Black-IsMouseOver-Background","#0072c6" },
                { "Accent-Black-IsMouseOver-BorderBrush","#0090f7" },
                { "Accent-Black-IsPressed-Background","#0067b0" },
                { "Accent-Black-IsPressed-BorderBrush","#0072c6" },
                { "Accent-Black-IsCheckedMoserOver-Background","#038BEC" },
                { "Accent-Black-IsCheckedMoserOver-BorderBrush","#0072c6" },
                { "Accent-Black-IsCheckedPressed-Background","#035D9D" },
                { "Accent-Black-IsCheckedPressed-BorderBrush","#0072c6" },
                { "Accent-White-IsDefaulted-BorderBrush","#106ebe" },
                { "Accent-White-IsMouseOver-Background","#cde6f7" },
                { "Accent-WHite-IsMouseOver-BorderBrush","#92c0e0" },
                { "Accent-White-IsPressed-Background","#92c0e0" },
                { "Accent-White-IsPressed-BorderBrush","#2a8dd4" },
                { "Accent-White-IsCheckedMoserOver-Background","#A1CDEC" },
                { "Accent-White-IsCheckedMoserOver-BorderBrush","#33A2F1" },
                { "Accent-White-IsCheckedPressed-Background","#88B3D1" },
                { "Accent-White-IsCheckedPressed-BorderBrush","#2580C1" },
            } },

            { AccentColors.Word, new Dictionary<string, string>(){
                { "Accent-Black-IsMouseOver-Background","#2b579a" },
                { "Accent-Black-IsMouseOver-BorderBrush","#3973cc" },
                { "Accent-Black-IsPressed-Background","#19478a" },
                { "Accent-Black-IsPressed-BorderBrush","#2b579a" },
                { "Accent-Black-IsCheckedMoserOver-Background","#4B81CE" },
                { "Accent-Black-IsCheckedMoserOver-BorderBrush","#2b579a" },
                { "Accent-Black-IsCheckedPressed-Background","#2A5CA5" },
                { "Accent-Black-IsCheckedPressed-BorderBrush","#2b579a" },
                { "Accent-White-IsDefaulted-BorderBrush","#2b579a" },
                { "Accent-White-IsMouseOver-Background","#d5e1f2" },
                { "Accent-WHite-IsMouseOver-BorderBrush","#a3bde3" },
                { "Accent-White-IsPressed-Background","#a3bde3" },
                { "Accent-White-IsPressed-BorderBrush","#3e6db5" },
                { "Accent-White-IsCheckedMoserOver-Background","#94B4E4" },
                { "Accent-White-IsCheckedMoserOver-BorderBrush","#4D80CE" },
                { "Accent-White-IsCheckedPressed-Background","#7DA5E1" },
                { "Accent-White-IsCheckedPressed-BorderBrush","#3863A5" },
            } },
            { AccentColors.Excel, new Dictionary<string, string>(){
                { "Accent-Black-IsMouseOver-Background","#217346" },
                { "Accent-Black-IsMouseOver-BorderBrush","#30a565" },
                { "Accent-Black-IsPressed-Background","#0a6332" },
                { "Accent-Black-IsPressed-BorderBrush","#217346" },
                { "Accent-Black-IsCheckedMoserOver-Background","#239D5A" },
                { "Accent-Black-IsCheckedMoserOver-BorderBrush","#217346" },
                { "Accent-Black-IsCheckedPressed-Background","#1B673E" },
                { "Accent-Black-IsCheckedPressed-BorderBrush","#217346" },
                { "Accent-White-IsDefaulted-BorderBrush","#217346" },
                { "Accent-White-IsMouseOver-Background","#d3f0e0" },
                { "Accent-WHite-IsMouseOver-BorderBrush","#86bfa0" },
                { "Accent-White-IsPressed-Background","#86bfa0" },
                { "Accent-White-IsPressed-BorderBrush","#3f8159" },
                { "Accent-White-IsCheckedMoserOver-Background","#ABCEBB" },
                { "Accent-White-IsCheckedMoserOver-BorderBrush","#4DA06D" },
                { "Accent-White-IsCheckedPressed-Background","#6BA887" },
                { "Accent-White-IsCheckedPressed-BorderBrush","#378556" },
            } },
            { AccentColors.PowerPoint, new Dictionary<string, string>(){
                { "Accent-Black-IsMouseOver-Background","#b83b1d" },
                { "Accent-Black-IsMouseOver-BorderBrush","#d24726" },
                { "Accent-Black-IsPressed-Background","#d24726" },
                { "Accent-Black-IsPressed-BorderBrush","#ff5630" },
                { "Accent-Black-IsCheckedMoserOver-Background","#E95B39" },
                { "Accent-Black-IsCheckedMoserOver-BorderBrush","#ff5630" },
                { "Accent-Black-IsCheckedPressed-Background","#D4401D" },
                { "Accent-Black-IsCheckedPressed-BorderBrush","#ff5630" },
                { "Accent-White-IsDefaulted-BorderBrush","#b7472a" },
                { "Accent-White-IsMouseOver-Background","#fce4dc" },
                { "Accent-WHite-IsMouseOver-BorderBrush","#f5ba9d" },
                { "Accent-White-IsPressed-Background","#f5ba9d" },
                { "Accent-White-IsPressed-BorderBrush","#dc5939" },
                { "Accent-White-IsCheckedMoserOver-Background","#F4C4AE" },
                { "Accent-White-IsCheckedMoserOver-BorderBrush","#DE6345" },
                { "Accent-White-IsCheckedPressed-Background","#F7A67E" },
                { "Accent-White-IsCheckedPressed-BorderBrush","#CE5537" },
            } },
            { AccentColors.Project, new Dictionary<string, string>(){
                { "Accent-Black-IsMouseOver-Background","#31752f" },
                { "Accent-Black-IsMouseOver-BorderBrush","#47a543" },
                { "Accent-Black-IsPressed-Background","#256323" },
                { "Accent-Black-IsPressed-BorderBrush","#31752f" },
                { "Accent-Black-IsCheckedMoserOver-Background","#50A84D" },
                { "Accent-Black-IsCheckedMoserOver-BorderBrush","#31752f" },
                { "Accent-Black-IsCheckedPressed-Background","#3B9238" },
                { "Accent-Black-IsCheckedPressed-BorderBrush","#31752f" },
                { "Accent-White-IsDefaulted-BorderBrush","#31752f" },
                { "Accent-White-IsMouseOver-Background","#cfedce" },
                { "Accent-WHite-IsMouseOver-BorderBrush","#8bbf8a" },
                { "Accent-White-IsPressed-Background","#8bbf8a" },
                { "Accent-White-IsPressed-BorderBrush","#4c944a" },
                { "Accent-White-IsCheckedMoserOver-Background","#B2E1B1" },
                { "Accent-White-IsCheckedMoserOver-BorderBrush","#76C973" },
                { "Accent-White-IsCheckedPressed-Background","#79AE78" },
                { "Accent-White-IsCheckedPressed-BorderBrush","#40803E" },
            } },
        }; // end property
    } // end class
} // end namespace
