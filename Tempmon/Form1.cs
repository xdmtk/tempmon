using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HardwareProviders;
using HardwareProviders.CPU;

namespace Tempmon
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        static void Print(Sensor[] sensors)
        {
            if (sensors.Any())
            {
                Console.WriteLine(string.Join(" ", sensors.Select(x => x.ToString())));
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            var cpu = Cpu.Discover();
            foreach(var item in cpu)
            {
                Print(item.CoreTemperatures);
            }


        }
    }
}
