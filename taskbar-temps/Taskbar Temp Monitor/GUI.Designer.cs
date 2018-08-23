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
            this.cpuImgHolder = new System.Windows.Forms.GroupBox();
            this.cpuPic = new System.Windows.Forms.PictureBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.cpuImgHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpuPic)).BeginInit();
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
            // cpuImgHolder
            // 
            this.cpuImgHolder.Controls.Add(this.cpuPic);
            this.cpuImgHolder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuImgHolder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cpuImgHolder.Location = new System.Drawing.Point(13, 38);
            this.cpuImgHolder.Name = "cpuImgHolder";
            this.cpuImgHolder.Size = new System.Drawing.Size(284, 238);
            this.cpuImgHolder.TabIndex = 1;
            this.cpuImgHolder.TabStop = false;
            this.cpuImgHolder.Text = "Manufacturer";
            this.cpuImgHolder.Enter += new System.EventHandler(this.cpuImgHolder_Enter);
            // 
            // cpuPic
            // 
            this.cpuPic.Location = new System.Drawing.Point(6, 16);
            this.cpuPic.Name = "cpuPic";
            this.cpuPic.Size = new System.Drawing.Size(272, 216);
            this.cpuPic.TabIndex = 0;
            this.cpuPic.TabStop = false;
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(312, 44);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(304, 226);
            this.treeView.TabIndex = 2;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Taskbar_Temp_Monitor.Properties.Resources.mainformbg;
            this.ClientSize = new System.Drawing.Size(640, 295);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.cpuImgHolder);
            this.Controls.Add(this.progName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.Text = "Taskbar Temp Monitor";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.cpuImgHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cpuPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label progName;
        private System.Windows.Forms.GroupBox cpuImgHolder;
        private System.Windows.Forms.PictureBox cpuPic;
        private System.Windows.Forms.TreeView treeView;
    }
}

