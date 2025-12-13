using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTetris.Models
{
    public class Unit
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Unit(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
