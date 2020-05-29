using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Segment
    {
        public bool free { get; private set; }
        byte[] bytes;
        int Size;
        public string Value 
        { 
            get
            {
                char[] value = new char[Size];
                for (int i = 0; i < Size; i++)
                {
                    if (bytes[i] == 0)
                        value[i] = '-';
                    else
                        value[i] = (char)bytes[i];
                }
                return new string(value);
            } 
        }
        public Segment(int size)
        {
            Size = size;
            Clear();
        }
        public void RandomFill()
        {
            free = false;
            Random random = new Random();
            random.NextBytes(bytes);
        }
        public void RandomFill(int to)
        {
            free = false;
            Random random = new Random();
            random.NextBytes(bytes);
            for (int i = to; i < bytes.Length; i++)
            {
                bytes[i] = 0;
            }

        }
        public void Clear()
        {
            free = true;
            bytes = new byte[Size];
        }
        public void MakeFree()
        {
            free = true;
        }
        public void Record(byte[] info)
        {
            free = false;
            bytes = info;
        }
    }
}
