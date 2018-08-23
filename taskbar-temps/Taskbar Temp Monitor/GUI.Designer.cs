namespace Taskbar_Temp_Monitor
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.progName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progName
            // 
            this.progName.AutoSize = true;
            this.progName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.progName.Location = new System.Drawing.Point(9, 5);
            this.progName.Name = "progName";
            this.progName.Size = new System.Drawing.Size(345, 19);
            this.progName.TabIndex = 0;
            this.progName.Text = "TASKBAR TEMPERATURE MONiTOR  v1.0";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Taskbar_Temp_Monitor.Properties.Resources.mainformbg;
            this.ClientSize = new System.Drawing.Size(640, 295);
            this.Controls.Add(this.progName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.Text = "Taskbar Temp Monitor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label progName;
    }
}

