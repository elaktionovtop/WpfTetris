using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace WpfTetris.Models
{
    public class GameBoard
    {
        public const int Columns = 10;
        public const int Rows = 20;

        public ObservableCollection<Cell> Cells { get; } = new ObservableCollection<Cell>();

        public GameBoard()
        {
            for(int i = 0; i < Columns * Rows; i++)
            {
                Cells.Add(new Cell()); // пустые клетки
            }
        }

        public void ShowUnit(Unit u)
        {
            // очищаем поле
            foreach(var cell in Cells)
                cell.Color = Brushes.Black;

            // показываем unit
            int index = u.Row * Columns + u.Col;
            Cells[index].Color = Brushes.Red;
        }
    }
}
