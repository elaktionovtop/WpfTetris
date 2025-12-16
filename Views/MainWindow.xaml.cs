using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

using WpfTetris.Models;

namespace WpfTetris
{
    public partial class MainWindow : Window
    {
        private GameBoard board;
        private Figure currentFigure;

        private DispatcherTimer timer;

        bool _isGameOver = false;

        public MainWindow()
        {
            InitializeComponent();

            board = new GameBoard();
            DataContext = board;

            currentFigure = CreateFigure();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        //bool CreateFigure()
        //{
        //    currentFigure = new Figure(0, board.Columns / 2 - 1);

        //    if(!board.CanPlace(currentFigure.Cells))
        //        return false;

        //    board.ShowFigure(currentFigure);
        //    return true;
        //}

        Figure CreateSquare()
        {
            int c = board.Columns / 2 - 1;
            return new Figure(
                new[]
                {
            (0, c),
            (0, c + 1),
            (1, c),
            (1, c + 1)
                },
                Brushes.Blue
            );
        }

        Figure CreateLine()
        {
            int c = board.Columns / 2;
            return new Figure(
                new[]
                {
            (0, c),
            (1, c),
            (2, c),
            (3, c)
                },
                Brushes.Cyan
            );
        }

        Figure CreateFigure()
        {
            Figure fig = Random.Shared.Next(2) == 0
                ? CreateSquare()
                : CreateLine();

            if(!board.CanPlace(fig.Cells))
                return null;

            board.ShowFigure(fig);
            return fig;
        }

        void TryRotate()
        {
            var rotated = currentFigure.GetRotated();

            if(!board.CanPlace(rotated))
                return;

            currentFigure.ApplyRotation(rotated);
            board.ShowFigure(currentFigure);
        }

        private void GameOver()
        {
            timer.Stop();
            _isGameOver = true;
            MessageBox.Show("Game Over");
        }

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
                currentFigure = CreateFigure();
                if(currentFigure == null)
                {
                    GameOver();
                }
            }

            board.ShowFigure(currentFigure);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
                TryRotate();

            if(e.Key == Key.Left) TryMove(0, -1);
            if(e.Key == Key.Right) TryMove(0, 1);
            if(e.Key == Key.Down) 
                while(TryMove(1, 0));
        }
        
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
            CreateFigure();
            timer.Start();
        }
    }
}
