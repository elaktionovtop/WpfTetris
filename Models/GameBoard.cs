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
                if (!cell.IsFilled)
                    cell.Color = Brushes.Black;

            // показываем unit
            int index = u.Row * Columns + u.Col;
            Cells[index].Color = Brushes.Red;
        }

        public bool IsFree(int row, int col)
        {
            return !Cells[row * Columns + col].IsFilled;
        }

        public void FixUnit(Unit u)
        {
            int index = u.Row * Columns + u.Col;
            Cells[index].IsFilled = true;
            Cells[index].Color = Brushes.Red;
        }

        public void CheckAndRemoveFullRows()
        {
            for(int row = Rows - 1; row >= 0; row--)
            {
                if(IsRowFull(row))
                {
                    RemoveRow(row);
                    row++; // перепроверяем эту же строку
                }
            }
        }

        private bool IsRowFull(int row)
        {
            for(int col = 0; col < Columns; col++)
                if(!Cells[row * Columns + col].IsFilled)
                    return false;

            return true;
        }

        private void RemoveRow(int row)
        {
            for(int r = row; r > 0; r--)
            {
                for(int c = 0; c < Columns; c++)
                {
                    int curr = r * Columns + c;
                    int above = (r - 1) * Columns + c;

                    Cells[curr].IsFilled = Cells[above].IsFilled;
                    Cells[curr].Color = Cells[above].Color;
                }
            }

            // верхнюю строку очистить
            for(int c = 0; c < Columns; c++)
            {
                Cells[c].IsFilled = false;
                Cells[c].Color = Brushes.Black;
            }
        }
    }
}
