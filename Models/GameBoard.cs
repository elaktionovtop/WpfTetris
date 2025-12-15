using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfTetris.Models
{
    public partial class GameBoard : ObservableObject
    {
        public readonly int Columns = 10;
        public readonly int Rows = 20;

        public ObservableCollection<Cell> Cells { get; } = new ObservableCollection<Cell>();

        [ObservableProperty]
        private int _score;
        
        public GameBoard()
        {
            for(int i = 0; i < Columns * Rows; i++)
            {
                Cells.Add(new Cell()); // пустые клетки
            }
        }

        //public void ShowUnit(Unit u)
        //{
        //    // очищаем поле
        //    foreach(var cell in Cells)
        //        if (!cell.IsFilled)
        //            cell.Color = Brushes.Black;

        //    // показываем unit
        //    int index = u.Row * Columns + u.Col;
        //    Cells[index].Color = Brushes.Red;
        //}

        public bool CanPlace(IEnumerable<(int Row, int Col)> cells)
        {
            foreach(var (row, col) in cells)
            {
                if(row < 0 || row >= Rows || col < 0 || col >= Columns)
                    return false;

                if(Cells[row * Columns + col].IsFilled)
                    return false;
            }
            return true;
        }
        
        public void ShowFigure(Figure figure)
        {
            foreach(var cell in Cells)
                if(!cell.IsFilled)
                    cell.Color = Brushes.Black;

            foreach(var (row, col) in figure.Cells)
                Cells[row * Columns + col].Color = figure.Color;
        }

        public bool IsFree(int row, int col)
        {
            return !Cells[row * Columns + col].IsFilled;
        }

        public void FixFigure(Figure figure)
        {
            foreach(var (row, col) in figure.Cells)
            {
                Cells[row * Columns + col].IsFilled = true;
                Cells[row * Columns + col].Color = figure.Color;
            }
        }
        
        //public void FixUnit(Unit u)
        //{
        //    int index = u.Row * Columns + u.Col;
        //    Cells[index].IsFilled = true;
        //    Cells[index].Color = Brushes.Red;
        //}

        public void CheckAndRemoveFullRows()
        {
            for(int row = Rows - 1; row >= 0; row--)
            {
                if(IsRowFull(row))
                {
                    RemoveRow(row);
                    Score += Columns;
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

        public void Clear()
        {
            foreach(var cell in Cells)
            {
                cell.IsFilled = false;
                cell.Color = Brushes.Black;
            }
            Score = 0;
        }
    }
}
