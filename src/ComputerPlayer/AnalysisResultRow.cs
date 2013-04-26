/// <summary>
/// Reversi.AnalysisResultRow.cs
/// </summary>

using System;
using System.Windows;
using System.Collections.Generic;

namespace Reversi
{
    /// <summary>
    /// A list of these is used to store the current progress of the computer players turn analysis
    /// </summary>
    public class AnalysisResultRow
    {
        public bool AnalysisCompleted = false;
        public double AnalysisResult = -9999;
    }
}