using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace WpfTetris.Models
{
    public class Figure
    {
        public List<(int Row, int Col)> Cells { get; }
        public Brush Color { get; }

        public Figure(int startRow, int startCol)
        {
            Color = Brushes.Blue;
            Cells = new List<(int, int)>
            {
                (startRow,     startCol),
                (startRow,     startCol + 1),
                (startRow + 1, startCol),
                (startRow + 1, startCol + 1)
            };
        }

        public void Move(int dRow, int dCol)
        {
            for(int i = 0; i < Cells.Count; i++)
            {
                var (r, c) = Cells[i];
                Cells[i] = (r + dRow, c + dCol);
            }
        }
    }
}
