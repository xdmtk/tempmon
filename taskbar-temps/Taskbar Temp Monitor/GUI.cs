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





        // Fork items
        NotifyIcon notify;
        ContextMenu context;
        MenuItem exitItem;
        MenuItem aboutItem;
        MenuItem showForm;
        Computer myComputer;
        Timer tempTicker;
        Timer titleTicker;
        Timer nodeTicker;
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
            this.nodeTicker = new Timer();
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
                CPUEnabled = true,
                HDDEnabled = true,
                MainboardEnabled = true,
                GPUEnabled = true
                
            };


            // Set timer interval and tick function
            tempTicker.Interval = 1000;
            tempTicker.Tick += new EventHandler(runMainLogicLoop);

            // Remove marquee
            //            titleTicker.Interval = 10;
            //            titleTicker.Tick += new EventHandler(marqueeTitle);

            nodeTicker.Tick += new EventHandler(updateTreeNodes);
            nodeTicker.Interval = 1000;
            


            // Add menu items to the contextMenu item (taskbar menu)
            this.context.MenuItems.AddRange(
                        new MenuItem[] { this.exitItem});



            // Set up menu items in taskbar
            this.exitItem.Index = 0;
            this.exitItem.Text = "E&xit";
            this.exitItem.Click += new EventHandler(this.menu_Click);




            // Start timer, initialize OHM object, and make taskbar icon visible
            myComputer.Open();
            tempTicker.Start();
            titleTicker.Start();
            setupTreeNodes(false);
            notify.ContextMenu = this.context;
            notify.Visible = true;
            nodeTicker.Start();


        }

        private void updateTreeNodes(object sender, EventArgs e)
        {
            setupTreeNodes(true);
        }
        TreeNode rootNode;
        TreeNode mainboard, cpu, hdd, gpu, fans;

        private void setupTreeNodes(bool update)
        {

            // Set up treeView nodes
            treeView.Font = SystemFonts.MessageBoxFont;
            ImageList nodeIcons = new ImageList();
            treeView.ImageList = nodeIcons;
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("computer.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("nvidia.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("ati.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("hdd.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("bigng.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("mainboard.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("chip.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("ram.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("gadget.png"));
            nodeIcons.Images.Add(OpenHardwareMonitor.Utilities.EmbeddedResources.GetImage("computer.png"));
            nodeIcons.Images.Add(Properties.Resources.Data_Flow_Chart_icon);


            if (update)
            {
                
                treeView.Nodes[0].Text = Environment.MachineName;
            }
            else
            {
                rootNode = treeView.Nodes.Add(Environment.MachineName);
                rootNode.ImageIndex = 0;
            }

            int count = 0;
            foreach (var hw in myComputer.Hardware)
            {
                if (update)
                {
                    switch (hw.HardwareType)
                    {
                        case HardwareType.CPU:
                            rootNode.Nodes[count].Text = (hw.Name);
                            setupSubNodes(hw, cpu, update);
                            break;
                        case HardwareType.Mainboard:
                            rootNode.Nodes[count].Text = (hw.Name);
                            setupSubNodes(hw, mainboard, update);
                            break;
                        case HardwareType.HDD:
                            rootNode.Nodes[count].Text = (hw.Name);
                            setupSubNodes(hw, hdd,update);
                            break;


                    }

                }
                else
                {
                    switch (hw.HardwareType)
                    {
                        case HardwareType.CPU:
                            cpu = rootNode.Nodes.Add(hw.Name);
                            cpu.ImageIndex = 6;
                            setupSubNodes(hw, cpu, update);
                            break;
                        case HardwareType.Mainboard:
                            mainboard = rootNode.Nodes.Add(hw.Name);
                            mainboard.ImageIndex = 5;
                            setupSubNodes(hw, mainboard, update);
                            break;
                        case HardwareType.HDD:
                            hdd = rootNode.Nodes.Add(hw.Name);
                            hdd.ImageIndex = 4;
                            setupSubNodes(hw, hdd,update);
                            break;


                    }
                }
            }
        }


        private void setupSubNodes(IHardware hardware, TreeNode subNode, bool update)
        {
                        //update ? (subNode.Nodes[count].Text = (sensor.Name + " - "  + ((decimal)sensor.Value).ToString() + "%");
           
            TreeNode nodelet;
            hardware.Update();
            int count = 0;
            foreach (var sensor in hardware.Sensors)
            {
                if (update)
                {
                    try
                    {
                        switch (sensor.SensorType)
                        {
                            case SensorType.Load:
                                subNode.Nodes[count].Text = (sensor.Name + " - " + ((decimal)sensor.Value).ToString() + "%");
                                break;

                            case SensorType.Clock:
                                subNode.Nodes[count].Text = (sensor.Name + " - " + sensor.Value.ToString() + "MHz");
                                break;

                            case SensorType.Temperature:
                                subNode.Nodes[count].Text = (sensor.Name + " - " + ((decimal)sensor.Value).ToString() + "C");
                                break;

                            case SensorType.Fan:
                                subNode.Nodes[count].Text = (sensor.Name + " - " + (sensor.Value).ToString() + " RPM");
                                break;

                            case SensorType.Power:
                                subNode.Nodes[count].Text = (sensor.Name + " - " + (sensor.Value).ToString() + "V");
                                break;

                            default:
                                subNode.Nodes[count].Text = (sensor.Name + " - " + (sensor.Value).ToString());
                                break;
                        }
                    }
                    catch
                    {
                        count++;
                        count--;
                    }
                }
                else
                {
                    switch (sensor.SensorType)
                    {
                        case SensorType.Load:
                            nodelet = subNode.Nodes.Add(sensor.Name + " - " + ((decimal)sensor.Value).ToString() + "%");
                            break;

                        case SensorType.Clock:
                            nodelet = subNode.Nodes.Add(sensor.Name + " - " + sensor.Value.ToString() + "MHz");
                            break;

                        case SensorType.Temperature:
                            nodelet = subNode.Nodes.Add(sensor.Name + " - " + ((decimal)sensor.Value).ToString() + "C");
                            break;

                        case SensorType.Fan:
                            nodelet = subNode.Nodes.Add(sensor.Name + " - " + (sensor.Value).ToString() + " RPM");
                            break;

                        case SensorType.Power:
                            nodelet = subNode.Nodes.Add(sensor.Name + " - " + (sensor.Value).ToString() + "V");
                            break;

                        default:
                            nodelet = subNode.Nodes.Add(sensor.Name + " - " + (sensor.Value).ToString());
                            break;

                    }
                    nodelet.ImageIndex = 10;
                }
                count++;
            }
        }
        private void runMainLogicLoop(object sender, EventArgs e)
        {

            // Every call to this function gets the current CPU temperature and
            // updates the icon and tooltip text
            string cpuName = "";
            decimal ctint;
            float? ct = getCpuTemp(ref cpuName);
            try
            {
                ctint = Math.Floor((decimal)ct);
            }
            catch
            {
                ctint = 0; 
            }
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
                                if (cpuName.ToLower().Contains("amd") && cpuName.ToLower().Contains("x"))
                                {
                                    return ((cpuTemp - 32) * (float?)(5.0 / 9.0));
                                }
                                else
                                {
                                    return cpuTemp;
                                }
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

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void kill_Icon(object sender, EventArgs e)
        {
            notify.Visible = false;
        }
    }



}
