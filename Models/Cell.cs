using System.Windows.Media;

namespace WpfTetris.Models
{
    public class Cell
    {
        public bool Filled { get; set; }
        public Brush Color { get; set; } = new SolidColorBrush
            (System.Windows.Media.Color.FromRgb(20, 20, 20));
    }
}
