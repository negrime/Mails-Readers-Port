using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    interface IThread
    {
        void Stop();
        void Pause();
        void Resume();
        
    }
}
