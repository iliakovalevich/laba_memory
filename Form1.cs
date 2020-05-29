using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<Process> processes;
        int MemorySize;
        int SegmentSize;
        Segment[] segments;
        public Form1()
        {
            InitializeComponent();
            processes = new List<Process>();
        }

        private void OutPut()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < segments.Length; i++)
            {
                dataGridView1.Rows.Add(new string[]
                {
                    "0x" + i.ToString("X4"),
                    segments[i].free ? "" : "######",
                    segments[i].Value
                });
            }
            int freememory = 0;
            foreach (var segment in segments)
            {
                if (segment.free)
                    freememory += SegmentSize;
            }
            label5.Text = freememory + "/"+ MemorySize + " kb";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MemorySize = (int)numericUpDown1.Value * 1024;
            SegmentSize = (int)numericUpDown2.Value * 1024;
            segments = new Segment[MemorySize / SegmentSize];
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i] = new Segment(SegmentSize);
            }
            OutPut();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = (int)numericUpDown3.Value;
            int address = FreePlace(size / SegmentSize);
            if (address == -1)
            {
                MessageBox.Show("Места нет", "Сообщение",  MessageBoxButtons.OK);
                return;
            }
            Record(new Process(address, size));
            OutPut();
        }
        private void Record(Process process)
        {
            processes.Add(process);
            int start = process.Address;
            int end = start + process.Size / SegmentSize;
            for (; start < end; start++)
            {
                segments[start].RandomFill();
            }
            segments[end].RandomFill(process.Size % SegmentSize);
        }
        private void Delete(Process process)
        {
            processes.Remove(process);
            int start = process.Address;
            int end = start + process.Size / SegmentSize;
            for (; start <= end; start++)
            {
                segments[start].MakeFree();
            }
        }
        private int FreePlace(int size)
        {
            int start = 0;
            int end = 0;
            while (end < segments.Length)
            {
                for (; start < segments.Length && !segments[start].free; start++) ;

                for (end = start; end < segments.Length && segments[end].free; end++)
                {
                    if (end - start >= size)
                    {
                        return start;
                    }
                }
            }
            return -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((int)numericUpDown4.Value >= processes.Count)
                return;
            Delete(processes[(int)numericUpDown4.Value]);
            OutPut();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Swap(ref segments[(int)numericUpDown5.Value], ref segments[(int)numericUpDown6.Value]);
            OutPut();
        }
        private void Swap(ref Segment a, ref Segment b)
        {
            Segment temp = a;
            a = b;
            b = temp;
        }
    }
}
