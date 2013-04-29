using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public enum AnalysisStatus : byte
    {
        QUEUED      = 1,    // Job has been queued
        WORKING     = 2,    // Job has been assigned a worker thread
        COMPLETE    = 3,    // Job has completed and is NOT currently the best option
        CHOSEN      = 9     // Job has completed and is currently the best option
    }
}
