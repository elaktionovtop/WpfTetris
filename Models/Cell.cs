using CommunityToolkit.Mvvm.ComponentModel;

using System.Windows.Media;

namespace WpfTetris.Models
{
    public partial class Cell : ObservableObject
    {
        public bool Filled { get; set; }
        
        [ObservableProperty]
        private Brush _color = new SolidColorBrush
            (System.Windows.Media.Color.FromRgb(20, 20, 20));
        //public Brush Color { get; set; } = new SolidColorBrush
        //    (System.Windows.Media.Color.FromRgb(20, 20, 20));
    }
}


