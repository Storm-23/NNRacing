namespace NNRacing
{
    partial class SimulationFrom
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
            this.mm = new System.Windows.Forms.MenuStrip();
            this.trackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btOpenTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.btSaveLeaderNN = new System.Windows.Forms.ToolStripMenuItem();
            this.btLoadNNandStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btTrackEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.btQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.btStart = new System.Windows.Forms.ToolStripMenuItem();
            this.cbFast = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudPopulation = new System.Windows.Forms.NumericUpDown();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lbBestTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbStat1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbStat2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbStat3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbStat4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPopulation)).BeginInit();
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mm
            // 
            this.mm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackToolStripMenuItem,
            this.btStart});
            this.mm.Location = new System.Drawing.Point(0, 0);
            this.mm.Name = "mm";
            this.mm.Size = new System.Drawing.Size(744, 24);
            this.mm.TabIndex = 1;
            this.mm.Text = "menuStrip1";
            // 
            // trackToolStripMenuItem
            // 
            this.trackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btOpenTrack,
            this.btSaveLeaderNN,
            this.btLoadNNandStart,
            this.toolStripMenuItem1,
            this.btTrackEditor,
            this.toolStripMenuItem2,
            this.btQuit});
            this.trackToolStripMenuItem.Name = "trackToolStripMenuItem";
            this.trackToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.trackToolStripMenuItem.Text = "File";
            // 
            // btOpenTrack
            // 
            this.btOpenTrack.Name = "btOpenTrack";
            this.btOpenTrack.Size = new System.Drawing.Size(157, 22);
            this.btOpenTrack.Text = "Open Track ...";
            this.btOpenTrack.Click += new System.EventHandler(this.btOpenTrack_Click);
            // 
            // btSaveLeaderNN
            // 
            this.btSaveLeaderNN.Name = "btSaveLeaderNN";
            this.btSaveLeaderNN.Size = new System.Drawing.Size(157, 22);
            this.btSaveLeaderNN.Text = "Save Leader NN";
            this.btSaveLeaderNN.Click += new System.EventHandler(this.btSaveLeaderNN_Click);
            // 
            // btLoadNNandStart
            // 
            this.btLoadNNandStart.Name = "btLoadNNandStart";
            this.btLoadNNandStart.Size = new System.Drawing.Size(157, 22);
            this.btLoadNNandStart.Text = "Load NN";
            this.btLoadNNandStart.Click += new System.EventHandler(this.btLoadNNandStart_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // btTrackEditor
            // 
            this.btTrackEditor.Name = "btTrackEditor";
            this.btTrackEditor.Size = new System.Drawing.Size(157, 22);
            this.btTrackEditor.Text = "Track Editor ...";
            this.btTrackEditor.Click += new System.EventHandler(this.btTrackEditor_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 6);
            // 
            // btQuit
            // 
            this.btQuit.Name = "btQuit";
            this.btQuit.Size = new System.Drawing.Size(157, 22);
            this.btQuit.Text = "Quit";
            this.btQuit.Click += new System.EventHandler(this.btQuit_Click);
            // 
            // btStart
            // 
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(96, 20);
            this.btStart.Text = "Start Evolution";
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // cbFast
            // 
            this.cbFast.AutoSize = true;
            this.cbFast.Location = new System.Drawing.Point(203, 3);
            this.cbFast.Name = "cbFast";
            this.cbFast.Size = new System.Drawing.Size(97, 17);
            this.cbFast.TabIndex = 2;
            this.cbFast.Text = "Fast Simulation";
            this.cbFast.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(335, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Population";
            // 
            // nudPopulation
            // 
            this.nudPopulation.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPopulation.Location = new System.Drawing.Point(398, 2);
            this.nudPopulation.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudPopulation.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPopulation.Name = "nudPopulation";
            this.nudPopulation.Size = new System.Drawing.Size(50, 20);
            this.nudPopulation.TabIndex = 4;
            this.nudPopulation.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbBestTime,
            this.lbStat1,
            this.lbStat2,
            this.lbStat3,
            this.lbStat4});
            this.ssMain.Location = new System.Drawing.Point(0, 462);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(744, 22);
            this.ssMain.TabIndex = 6;
            this.ssMain.Text = "statusStrip1";
            // 
            // lbBestTime
            // 
            this.lbBestTime.AutoSize = false;
            this.lbBestTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbBestTime.Name = "lbBestTime";
            this.lbBestTime.Size = new System.Drawing.Size(120, 17);
            // 
            // lbStat1
            // 
            this.lbStat1.AutoSize = false;
            this.lbStat1.Name = "lbStat1";
            this.lbStat1.Size = new System.Drawing.Size(120, 17);
            // 
            // lbStat2
            // 
            this.lbStat2.AutoSize = false;
            this.lbStat2.Name = "lbStat2";
            this.lbStat2.Size = new System.Drawing.Size(120, 17);
            // 
            // lbStat3
            // 
            this.lbStat3.AutoSize = false;
            this.lbStat3.Name = "lbStat3";
            this.lbStat3.Size = new System.Drawing.Size(120, 17);
            // 
            // lbStat4
            // 
            this.lbStat4.AutoSize = false;
            this.lbStat4.Name = "lbStat4";
            this.lbStat4.Size = new System.Drawing.Size(120, 17);
            // 
            // SimulationFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(744, 484);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudPopulation);
            this.Controls.Add(this.cbFast);
            this.Controls.Add(this.mm);
            this.Name = "SimulationFrom";
            this.Text = "Simulation";
            this.mm.ResumeLayout(false);
            this.mm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPopulation)).EndInit();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mm;
        private System.Windows.Forms.ToolStripMenuItem trackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btOpenTrack;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btQuit;
        private System.Windows.Forms.ToolStripMenuItem btTrackEditor;
        private System.Windows.Forms.ToolStripMenuItem btStart;
        private System.Windows.Forms.CheckBox cbFast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudPopulation;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lbStat1;
        private System.Windows.Forms.ToolStripStatusLabel lbStat2;
        private System.Windows.Forms.ToolStripStatusLabel lbStat3;
        private System.Windows.Forms.ToolStripStatusLabel lbStat4;
        private System.Windows.Forms.ToolStripStatusLabel lbBestTime;
        private System.Windows.Forms.ToolStripMenuItem btSaveLeaderNN;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem btLoadNNandStart;
    }
}