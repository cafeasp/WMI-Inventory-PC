using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventory
{
    class Processor
    {
        enum CpuArchitecture
        {
            x86,MIPS,Alpha,PowerPC,IPF,x64
        }

        public string Architecture { get; set; }

    }
}
