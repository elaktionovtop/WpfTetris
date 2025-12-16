using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace WpfTetris.Models
{
    public class Figure
    {
//        public List<(int Row, int Col)> Cells { get; }
        public List<(int Row, int Col)> Cells { get; private set; }
        public Brush Color { get; }

        //public Figure(int startRow, int startCol)
        //{
        //    Color = Brushes.Blue;
        //    Cells = new List<(int, int)>
        //    {
        //        (startRow,     startCol),
        //        (startRow,     startCol + 1),
        //        (startRow + 1, startCol),
        //        (startRow + 1, startCol + 1)
        //    };
        //}

        public Figure(IEnumerable<(int Row, int Col)> cells, Brush color)
        {
            Cells = cells.ToList();
            Color = color;
        }

        public void Move(int dRow, int dCol)
        {
            for(int i = 0; i < Cells.Count; i++)
            {
                var (r, c) = Cells[i];
                Cells[i] = (r + dRow, c + dCol);
            }
        }

        public IEnumerable<(int Row, int Col)> GetRotated()
        {
            // pivot = Cells[0]
            var (pr, pc) = Cells[0];

            foreach(var (r, c) in Cells)
            {
                int dr = r - pr;
                int dc = c - pc;

                // поворот на 90° по часовой
                yield return (pr - dc, pc + dr);
            }
        }

        public void ApplyRotation(IEnumerable<(int, int)> rotated)
        {
            Cells = rotated.ToList();
        }
    }
}
