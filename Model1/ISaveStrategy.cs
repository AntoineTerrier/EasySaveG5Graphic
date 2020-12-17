using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public interface ISaveStrategy
{
    string Save(string sourceDir, string targetDir, Semaphore MaxSizeSemaphore);

}
