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

            notify.Icon = Properties.Resources.icon_template;
            notify.ContextMenu = this.context;
            notify.Text = "Form 1 Example";
            notify.Visible = true;

            notify.DoubleClick += new EventHandler(this.notify_DoubleClick);

            tempTicker.Start();


        }


        private void runMainLogicLoop(object sender, EventArgs e)
        {
            float? ct = getCpuTemp();
            if (ct < 25)
            {
                notify.Icon = Properties.Resources.cpu_iconpurple1_256px;
            }
            else if (ct >= 25 && ct < 30)
            {
                notify.Icon = Properties.Resources.cpu_iconblue1_256px;
            } 
            else if (ct >= 30 && ct < 39) 
            {
                notify.Icon = Properties.Resources.cpu_icongreen1_256px;
            } 
            else if (ct >= 40 && ct < 44) 
            {
                notify.Icon = Properties.Resources.cpu_iconyellow1_256px;
            } 
            else if (ct >= 44 && ct < 50) 
            {
                notify.Icon = Properties.Resources.cpu_iconorange1_256px;
            }
            else if (ct >= 50 && ct < 70) 
            {
                notify.Icon = Properties.Resources.cpu_iconred1_256px;
            } 
            else if (ct >= 70) 
            {
                notify.Icon = Properties.Resources.danger_icon;
            } 
            else
            {
                notify.Icon = Properties.Resources.question;
                notify.Text = "Unsupported CPU";
                return;
            }
            notify.Text = ct.ToString();
            return;
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
