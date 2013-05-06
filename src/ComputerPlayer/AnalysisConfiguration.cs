using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    static class AnalysisConfiguration
    {
        // This is an attempt to rate the value of each spot on the board
        public static readonly int[,] BoardValueMask = new int[,]
            {
	            {25, 0,10,10,10,10, 0,25},
   	            { 0, 0, 1, 1, 1, 1, 0, 0},
   	            {10, 1, 6, 3, 3, 6, 1,10},
   	            {10, 1, 3, 0, 0, 3, 1,10},
   	            {10, 1, 3, 0, 0, 3, 1,10},
   	            {10, 1, 6, 3, 3, 6, 1,10},
   	            { 0, 0, 1, 1, 1, 1, 0, 0},
	            {25, 0,10,10,10,10, 0,25}
            };

        public static readonly byte VictoryWeight = 10;
    }
}
