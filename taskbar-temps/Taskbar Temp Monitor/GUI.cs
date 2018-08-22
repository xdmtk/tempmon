using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.WMI;
using OpenHardwareMonitor.Utilities;

namespace Taskbar_Temp_Monitor
{
   

    public partial class GUI : Form
    {
        Icon[] tempArray = new Icon[]
        {
          Properties.Resources._30,
          Properties.Resources._31,
          Properties.Resources._32,
          Properties.Resources._33,
          Properties.Resources._34,
          Properties.Resources._35,
          Properties.Resources._36,
          Properties.Resources._37,
          Properties.Resources._38,
          Properties.Resources._39,
          Properties.Resources._40,
          Properties.Resources._41,
          Properties.Resources._42,
          Properties.Resources._43,
          Properties.Resources._44,
          Properties.Resources._45,
          Properties.Resources._46,
          Properties.Resources._47,
          Properties.Resources._48,
          Properties.Resources._49,
          Properties.Resources._50,
          Properties.Resources._51,
          Properties.Resources._52,
          Properties.Resources._53,
          Properties.Resources._54,
          Properties.Resources._55,
          Properties.Resources._56,
          Properties.Resources._57,
          Properties.Resources._58,
          Properties.Resources._59,
          Properties.Resources._50,
          Properties.Resources._61,
          Properties.Resources._62,
          Properties.Resources._63,
          Properties.Resources._64,
          Properties.Resources._65,
          Properties.Resources._66,
          Properties.Resources._67,
          Properties.Resources._68,
          Properties.Resources._69,
          Properties.Resources._70,
        };

        NotifyIcon notify;
        ContextMenu context;
        MenuItem menu;
        Computer myComputer;
        Timer tempTicker;
        
        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated)
            {
                value = false;
                CreateHandle();

            }
            base.SetVisibleCore(value);
        }

        public GUI()
        {
            InitializeComponent();
            this.components = new Container();
            this.context = new ContextMenu();
            this.menu = new MenuItem();
            this.tempTicker = new Timer();
            tempTicker.Interval = 100;
            tempTicker.Tick += new EventHandler(this.runMainLogicLoop);


            this.context.MenuItems.AddRange(
                        new MenuItem[] { this.menu });

            this.menu.Index = 0;
            this.menu.Text = "E&xit";
            this.menu.Click += new EventHandler(this.menu_Click);

            this.notify = new NotifyIcon(this.components);

            myComputer = new Computer
            {
                CPUEnabled = true
            };
        
            myComputer.Open();

            notify.ContextMenu = this.context;
            notify.Text = "Form 1 Example";
            notify.Visible = true;

            notify.DoubleClick += new EventHandler(this.notify_DoubleClick);

            tempTicker.Start();


        }


        private void runMainLogicLoop(object sender, EventArgs e)
        {
            float? ct = getCpuTemp();
            decimal ctint = Math.Floor((decimal)ct);
            if (ctint < 30)
            {
                notify.Icon = Properties.Resources.frost;
            }
            else if (ctint > 70)
            {
                notify.Icon = Properties.Resources.fire;
            }
            else
            {
                for (int i = 30; i <= 70; ++i)
                {
                    if (ctint == i)
                    {
                        notify.Icon = tempArray[i - 30];
                    }
                }
            }
            notify.Text = ctint.ToString();




        }
        private float? getCpuTemp()
        {
           float? cpuTemp;
           foreach (var hardware in myComputer.Hardware)
           {
                hardware.Update();
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    foreach (var sensor in hardware.Sensors)
                    {
                       if (sensor.SensorType == SensorType.Temperature)
                        {
                            if (sensor.Value != null)
                            {
                                cpuTemp = sensor.Value;
                                return ((cpuTemp - 32) * (float?)(5.0/9.0));
                            }
                            else
                            {
                                return null;
                            }

                        }
                     
                    }
                }
            }
            return null;
        }



        private void menu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notify_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
            
        }


    }



}
