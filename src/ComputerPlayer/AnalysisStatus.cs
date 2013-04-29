using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public enum AnalysisStatus : byte
    {
        STARTED     = 1,
        WORKING     = 2,
        CHOSEN      = 3,
        COMPLETE    = 9
    }
}
