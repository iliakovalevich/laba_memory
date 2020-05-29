using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Process
    {
        public int Address;
        public int Size;
        public Process(int address, int size)
        {
            Address = address;
            Size = size;
        }
    }
}
