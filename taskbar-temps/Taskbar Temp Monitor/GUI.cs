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
        Timer titleTicker;
        ContextMenuStrip formContext;

        private bool md;
        private int xPos = 0, yPos = 5;
        private Point lastLocation;
        
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
            this.titleTicker = new Timer();
            this.notify = new NotifyIcon(this.components);
            progName.BackColor = Color.Transparent;
            cpuImgHolder.BackColor = Color.Transparent;
            
            this.Opacity = 80;
            this.AllowTransparency = true;

            // Make borderless form draggable
            this.MouseDown += new MouseEventHandler(form_Drag);
            this.MouseMove += new MouseEventHandler(form_Move);
            this.MouseUp += new MouseEventHandler(form_DragComplete);
            progName.MouseDown += new MouseEventHandler(form_Drag);
            progName.MouseMove += new MouseEventHandler(form_Move);
            progName.MouseUp += new MouseEventHandler(form_DragComplete);
            cpuPic.MouseDown += new MouseEventHandler(form_Drag);
            cpuPic.MouseMove += new MouseEventHandler(form_Move);
            cpuPic.MouseUp += new MouseEventHandler(form_DragComplete);
            cpuImgHolder.MouseDown += new MouseEventHandler(form_Drag);
            cpuImgHolder.MouseMove += new MouseEventHandler(form_Move);
            cpuImgHolder.MouseUp += new MouseEventHandler(form_DragComplete);
            

            // Kill Icon on closing
            this.FormClosing += new FormClosingEventHandler(kill_Icon);


            // Open menu on form right click
            this.MouseClick += new MouseEventHandler(openExitMenu);
            cpuImgHolder.MouseClick += new MouseEventHandler(openExitMenu);
            cpuPic.MouseClick += new MouseEventHandler(openExitMenu);
            progName.MouseClick += new MouseEventHandler(openExitMenu);
            
            // OpenHardwareMonitor Object
            this.myComputer = new Computer
            {
                CPUEnabled = true
            };


            // Set timer interval and tick function
            tempTicker.Interval = 100;
            tempTicker.Tick += new EventHandler(runMainLogicLoop);
            titleTicker.Interval = 10;
            titleTicker.Tick += new EventHandler(marqueeTitle);

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
            titleTicker.Start();
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

            setFormPic(cpuName);




         }
       
        
        // Set either AMD or Intel pic
        private void setFormPic(string cpuName)
        {
            cpuPic.SizeMode = PictureBoxSizeMode.StretchImage;
            if (cpuName.ToLower().Contains("amd"))
            {
                cpuPic.Image = Properties.Resources.amd_icon;
            }
            else
            {
                cpuPic.Image = Properties.Resources.intel_icon;
            }
        }


        // OHM Logic to get CPU temperature
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
            


        // Drag form functions
        private void form_Drag(object sender, MouseEventArgs e)
        {
                md = true;
                lastLocation = e.Location;
        }

        private void form_Move(object sender, MouseEventArgs e)
        {

            // Allow dragging from Form and Title Label
            if (sender.GetType().Name == "GUI")
            {
                if (md)
                {
                    this.Location = new Point(
                        (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                    this.Update();
                }
            }
            else
            {
                if (md)
                {
                    Form thisForm = ActiveControl.FindForm();
                    thisForm.Location = new Point(
                        (thisForm.Location.X - lastLocation.X) + e.X, (thisForm.Location.Y - lastLocation.Y) + e.Y);
                    thisForm.Update();
                }
            }
        }

        private void form_DragComplete(object sender, MouseEventArgs e)
        {
            md = false;
        }
        private void openExitMenu(object sender, MouseEventArgs e)
        {

            Point currentLoc = new Point(e.Location.X, e.Location.Y);
            if (e.Button == MouseButtons.Right)
            {
                context.Show(this,currentLoc);
            }
        }
        private void cpuImgHolder_Enter(object sender, EventArgs e)
        {

        }

        private void marqueeTitle(object sender, EventArgs e)
        {

            if (xPos <= ((-1*(progName.Width))))
            {

                progName.Location = new Point(this.Width-(progName.Width/8), yPos);
                xPos = this.Width+progName.Width;
            }
            else
            {
                progName.Location = new Point(xPos, yPos);
                xPos -= 2;
            }

        }

        private void kill_Icon(object sender, EventArgs e)
        {
            notify.Visible = false;
        }
    }



}
