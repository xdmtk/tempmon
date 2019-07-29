namespace Tempmon
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.currentInfo = new System.Windows.Forms.GroupBox();
            this.averageInfo = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(11, 11);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(501, 409);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "tempChart";
            // 
            // currentInfo
            // 
            this.currentInfo.Location = new System.Drawing.Point(521, 15);
            this.currentInfo.Name = "currentInfo";
            this.currentInfo.Size = new System.Drawing.Size(270, 151);
            this.currentInfo.TabIndex = 1;
            this.currentInfo.TabStop = false;
            this.currentInfo.Text = "Current";
            // 
            // averageInfo
            // 
            this.averageInfo.Location = new System.Drawing.Point(521, 172);
            this.averageInfo.Name = "averageInfo";
            this.averageInfo.Size = new System.Drawing.Size(270, 248);
            this.averageInfo.TabIndex = 2;
            this.averageInfo.TabStop = false;
            this.averageInfo.Text = "Stats";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 431);
            this.Controls.Add(this.averageInfo);
            this.Controls.Add(this.currentInfo);
            this.Controls.Add(this.chart1);
            this.Name = "MainForm";
            this.Text = "Tempmon";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox currentInfo;
        private System.Windows.Forms.GroupBox averageInfo;
    }
}

