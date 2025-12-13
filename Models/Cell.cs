using CommunityToolkit.Mvvm.ComponentModel;

using System.Windows.Media;

namespace WpfTetris.Models
{
    public partial class Cell : ObservableObject
    {
        [ObservableProperty]
        private Brush _color = new SolidColorBrush
            (System.Windows.Media.Color.FromRgb(20, 20, 20));

        [ObservableProperty]
        private bool _isFilled;
    }
}


