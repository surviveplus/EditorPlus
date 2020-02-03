using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeThemeSample
{
    public class OfficeTheme  : INotifyPropertyChanged
    {
        public static OfficeTheme Current { get; set; } = new OfficeTheme();

        public OfficeTheme()
        {
            this.Theme = Theme.Black;
        }

        private Theme valueOfTheme;

        public Theme Theme
        {
            get => this.valueOfTheme;
            set
            {
                this.valueOfTheme = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Current"));
            } // end set
        } // end property 

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum Theme {
        Colorful = 0,
        DarkGray = 3,
        Black = 4,
        White = 5,
    }
}
