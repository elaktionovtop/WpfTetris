using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using WpfTetris.Models;

namespace WpfTetris
{
    public partial class MainWindow : Window
    {
        private GameBoard board;
        //private Unit unit;
        private Figure currentFigure;

        private DispatcherTimer timer;

        bool _isGameOver = false;

        public MainWindow()
        {
            InitializeComponent();

            board = new GameBoard();
            DataContext = board;
            //BoardGrid.ItemsSource = board.Cells;

            //CreateUnit();
            CreateFigure();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        //private void CreateUnit()
        //{
        //    int startRow = 0;
        //    int startCol = GameBoard.Columns / 2;

        //    if(!board.IsFree(startRow, startCol))
        //    {
        //        GameOver();
        //        return;
        //    }

        //    unit = new Unit(startRow, startCol);
        //    board.ShowUnit(unit);
        //}

        //void CreateFigure()
        //{
        //    currentFigure = new Figure(0, board.Columns / 2 - 1);
        //    board.ShowFigure(currentFigure);
        //}

        bool CreateFigure()
        {
            currentFigure = new Figure(0, board.Columns / 2 - 1);

            if(!board.CanPlace(currentFigure.Cells))
                return false;

            board.ShowFigure(currentFigure);
            return true;
        }

        private void GameOver()
        {
            timer.Stop();
            _isGameOver = true;
            MessageBox.Show("Game Over");
        }

        //private void Timer_Tick(object? sender, EventArgs e)
        //{
        //    if(CanMoveDown())
        //    {
        //        unit.Row++;
        //        board.ShowUnit(unit);
        //    }
        //    else
        //    {
        //        board.FixUnit(unit);
        //        board.CheckAndRemoveFullRows();
        //        CreateUnit();
        //    }
        //}

        private void Timer_Tick(object sender, EventArgs e)
        {
            var next = currentFigure.Cells
                .Select(c => (c.Row + 1, c.Col));

            if(board.CanPlace(next))
            {
                currentFigure.Move(1, 0);
            }
            else
            {
                board.FixFigure(currentFigure);
                board.CheckAndRemoveFullRows();
                //CreateFigure();
                if(!CreateFigure())
                {
                    GameOver();
                }
            }

            board.ShowFigure(currentFigure);
        }


        //private bool CanMoveDown()
        //{
        //    int nextRow = unit.Row + 1;
        //    if(nextRow >= GameBoard.Rows) return false;

        //    return board.IsFree(nextRow, unit.Col);
        //}

        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch(e.Key)
        //    {
        //        case Key.Left:
        //            TryMove(0, -1);
        //            break;

        //        case Key.Right:
        //            TryMove(0, 1);
        //            break;

        //        case Key.Down:
        //            while(CanMoveDown())
        //            {
        //                unit.Row++;
        //            }
        //            board.ShowUnit(unit); break;
        //    }
        //}

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left) TryMove(0, -1);
            if(e.Key == Key.Right) TryMove(0, 1);
            if(e.Key == Key.Down) 
                while(TryMove(1, 0));
        }
        
        //private void TryMove(int dRow, int dCol)
        //{
        //    int newRow = unit.Row + dRow;
        //    int newCol = unit.Col + dCol;

        //    if(newRow < 0 || newRow >= GameBoard.Rows) return;
        //    if(newCol < 0 || newCol >= GameBoard.Columns) return;

        //    unit.Row = newRow;
        //    unit.Col = newCol;

        //    board.ShowUnit(unit);
        //}

        //void TryMove(int dRow, int dCol)
        //{
        //    var next = currentFigure.Cells
        //        .Select(c => (c.Row + dRow, c.Col + dCol));

        //    if(!board.CanPlace(next))
        //        return;

        //    currentFigure.Move(dRow, dCol);
        //    board.ShowFigure(currentFigure);
        //}

        bool TryMove(int dRow, int dCol)
        {
            var next = currentFigure.Cells
                .Select(c => (c.Row + dRow, c.Col + dCol));

            if(!board.CanPlace(next))
                return false;

            currentFigure.Move(dRow, dCol);
            board.ShowFigure(currentFigure);
            return true;
        }
        
        private bool isPaused = false;

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if(isPaused)
            {
                timer.Start();
                isPaused = false;
            }
            else
            {
                timer.Stop();
                isPaused = true;
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            board.Clear();
            //CreateUnit();
            CreateFigure();
            timer.Start();
        }
    }
}
