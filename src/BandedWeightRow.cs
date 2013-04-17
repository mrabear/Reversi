// Reversi
// Brian Hebert
//

using System;
using System.Drawing;
using System.Collections.Generic;

namespace Reversi
{
    // A list of these is used to calculated banded averages of game states
    public class BandedWeightRow
    {
        public int NodeCount;
        public double TotalWeight;

        public BandedWeightRow()
        {
            NodeCount = 0;
            TotalWeight = 0;
        }

        public BandedWeightRow(double NewWeight)
        {
            NodeCount = 1;
            TotalWeight = NewWeight;
        }

        public BandedWeightRow(int NewWeight)
        {
            NodeCount = 1;
            TotalWeight = NewWeight;
        }
    }
}