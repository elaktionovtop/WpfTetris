using System.Windows;
using System.Windows.Threading;

using WpfTetris.Models;

namespace WpfTetris
{
    public partial class MainWindow : Window
    {
        private GameBoard board;
        private Unit unit;

        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            board = new GameBoard();
            BoardGrid.ItemsSource = board.Cells;

            CreateUnit();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void CreateUnit()
        {
            unit = new Unit(0, GameBoard.Columns / 2);
            board.ShowUnit(unit);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if(CanMoveDown())
            {
                unit.Row++;
                board.ShowUnit(unit);
            }
            else
            {
                // пока просто останавливаемся
                timer.Stop();
            }
        }

        private bool CanMoveDown()
        {
            return unit.Row < GameBoard.Rows - 1;
        }
    }
}