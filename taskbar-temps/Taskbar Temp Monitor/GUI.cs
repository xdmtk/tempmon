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
        MenuItem exitItem;
        MenuItem aboutItem;
        MenuItem showForm;
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

            // Initialize all controls for application
            this.components = new Container();
            this.context = new ContextMenu();
            this.exitItem = new MenuItem();
            this.showForm = new MenuItem();
            this.aboutItem = new MenuItem();
            this.tempTicker = new Timer();
            this.notify = new NotifyIcon(this.components);

            // OpenHardwareMonitor Object
            this.myComputer = new Computer
            {
                CPUEnabled = true
            };


            // Set timer interval and tick function
            tempTicker.Interval = 100;
            tempTicker.Tick += new EventHandler(this.runMainLogicLoop);

            // Add menu items to the contextMenu item (taskbar menu)
            this.context.MenuItems.AddRange(
                        new MenuItem[] { this.exitItem, this.showForm, this.aboutItem});



            // Set up menu items in taskbar
            this.exitItem.Index = 2;
            this.exitItem.Text = "E&xit";
            this.exitItem.Click += new EventHandler(this.menu_Click);

            this.aboutItem.Index = 1;
            this.aboutItem.Text = "A&bout";
            this.aboutItem.Click += new EventHandler(this.about_Click);

            this.showForm.Index = 0;
            this.showForm.Text = "O&pen";
            this.showForm.Click += new EventHandler(this.showForm_Click);

        


            // Start timer, initialize OHM object, and make taskbar icon visible
            myComputer.Open();
            tempTicker.Start();
            notify.ContextMenu = this.context;
            notify.Visible = true;


        }


        private void runMainLogicLoop(object sender, EventArgs e)
        {

            // Every call to this function gets the current CPU temperature and
            // updates the icon and tooltip text
            string cpuName = "";
            float? ct = getCpuTemp(ref cpuName);
            decimal ctint = Math.Floor((decimal)ct);

            // Too cold.. show frost
            if (ctint < 30)
            {
                notify.Icon = Properties.Resources.frost;
            }
            // Too hot.. show fire
            else if (ctint > 70)
            {
                notify.Icon = Properties.Resources.fire;
            }

            // Otherwise search through the array of number icons
            // in resources to update the current icon with the corresponding
            // temperature icon
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


            notify.Text = cpuName + ": " + ctint.ToString() + "C";
            




         }
        

        private float? getCpuTemp(ref string cpuName)
        {
           float? cpuTemp;
           foreach (var hardware in myComputer.Hardware)
           {
                hardware.Update();
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    cpuName = hardware.Name.ToString(); 
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

        private void showForm_Click(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                this.Show();
            }
        }
    

        private void about_Click(object sender, EventArgs e)
        {
            // Add about form code here
        }
        private void menu_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }



}
