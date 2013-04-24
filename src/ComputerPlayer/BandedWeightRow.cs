/// <summary>
/// Reversi.BandedWeightRow.cs
/// </summary>

using System;
using System.Drawing;
using System.Collections.Generic;

namespace Reversi
{
    /// <summary>
    /// A list of these is used to calculated banded averages of game states
    /// </summary>
    public class BandedWeightRow
    {
        public int NodeCount;
        public double TotalWeight;

        /// <summary>
        /// Creates a new BandedRow with 0 weight
        /// </summary>
        public BandedWeightRow()
        {
            NodeCount = 0;
            TotalWeight = 0;
        }

        /// <summary>
        /// Creates a new BandedRow with the given weight
        /// </summary>
        /// <param name="NewWeight">The initial weight of this row</param>
        public BandedWeightRow(double NewWeight)
        {
            NodeCount = 1;
            TotalWeight = NewWeight;
        }

        /// <summary>
        /// Creates a new BandedRow with the given weight
        /// </summary>
        /// <param name="NewWeight">The initial weight of this row</param>
        public BandedWeightRow(int NewWeight)
        {
            NodeCount = 1;
            TotalWeight = NewWeight;
        }
    }
}