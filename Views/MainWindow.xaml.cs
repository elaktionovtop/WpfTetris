using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;

using WpfTetris.Models;

namespace WpfTetris
{
    public partial class MainWindow : Window
    {
        private GameBoard board;
        private Unit unit;

        public MainWindow()
        {
            InitializeComponent();

            board = new GameBoard();
            BoardGrid.ItemsSource = board.Cells;

            CreateUnit();
        }

        private void CreateUnit()
        {
            unit = new Unit(0, GameBoard.Columns / 2);
            board.ShowUnit(unit);
        }
    }
}