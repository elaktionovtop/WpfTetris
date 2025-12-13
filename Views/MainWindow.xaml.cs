using System.Windows;
using System.Windows.Input;
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
                board.FixUnit(unit);
                board.CheckAndRemoveFullRows();
                CreateUnit();
            }
        }

        private bool CanMoveDown()
        {
            int nextRow = unit.Row + 1;
            if(nextRow >= GameBoard.Rows) return false;

            return board.IsFree(nextRow, unit.Col);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left:
                    TryMove(0, -1);
                    break;

                case Key.Right:
                    TryMove(0, 1);
                    break;

                case Key.Down:
                    TryMove(1, 0);
                    break;
            }
        }

        private void TryMove(int dRow, int dCol)
        {
            int newRow = unit.Row + dRow;
            int newCol = unit.Col + dCol;

            if(newRow < 0 || newRow >= GameBoard.Rows) return;
            if(newCol < 0 || newCol >= GameBoard.Columns) return;

            unit.Row = newRow;
            unit.Col = newCol;

            board.ShowUnit(unit);
        }
    }
}
