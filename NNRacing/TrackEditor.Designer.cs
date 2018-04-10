namespace NNRacing
{
    partial class TrackEditor
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
            this.btOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.btSave = new System.Windows.Forms.ToolStripMenuItem();
            this.btNew = new System.Windows.Forms.ToolStripMenuItem();
            this.btDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.btEditTrack = new System.Windows.Forms.ToolStripMenuItem();
            this.btDeletePoint = new System.Windows.Forms.ToolStripMenuItem();
            this.btInsertPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.paddingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ss = new System.Windows.Forms.StatusStrip();
            this.lbTrackLength = new System.Windows.Forms.ToolStripStatusLabel();
            this.nudTrackWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudTension = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudAdhesion = new System.Windows.Forms.NumericUpDown();
            this.mm.SuspendLayout();
            this.ss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrackWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTension)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdhesion)).BeginInit();
            this.SuspendLayout();
            // 
            // mm
            // 
            this.mm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackToolStripMenuItem,
            this.btDraw,
            this.btEditTrack,
            this.btDeletePoint,
            this.btInsertPoint,
            this.paddingToolStripMenuItem});
            this.mm.Location = new System.Drawing.Point(0, 0);
            this.mm.Name = "mm";
            this.mm.Size = new System.Drawing.Size(800, 24);
            this.mm.TabIndex = 0;
            this.mm.Text = "menuStrip1";
            // 
            // trackToolStripMenuItem
            // 
            this.trackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNew,
            this.btOpen,
            this.btSave});
            this.trackToolStripMenuItem.Name = "trackToolStripMenuItem";
            this.trackToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.trackToolStripMenuItem.Text = "File";
            // 
            // btOpen
            // 
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(152, 22);
            this.btOpen.Text = "Open ...";
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // btSave
            // 
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(152, 22);
            this.btSave.Text = "Save ...";
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btNew
            // 
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(152, 22);
            this.btNew.Text = "New ...";
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // btDraw
            // 
            this.btDraw.Name = "btDraw";
            this.btDraw.Size = new System.Drawing.Size(78, 20);
            this.btDraw.Text = "Draw Track";
            this.btDraw.Click += new System.EventHandler(this.btDraw_Click);
            // 
            // btEditTrack
            // 
            this.btEditTrack.Name = "btEditTrack";
            this.btEditTrack.Size = new System.Drawing.Size(71, 20);
            this.btEditTrack.Text = "Edit Track";
            this.btEditTrack.Click += new System.EventHandler(this.btEditTrack_Click);
            // 
            // btDeletePoint
            // 
            this.btDeletePoint.Name = "btDeletePoint";
            this.btDeletePoint.Size = new System.Drawing.Size(83, 20);
            this.btDeletePoint.Text = "Delete Point";
            this.btDeletePoint.Click += new System.EventHandler(this.btDeletePoint_Click);
            // 
            // btInsertPoint
            // 
            this.btInsertPoint.Name = "btInsertPoint";
            this.btInsertPoint.Size = new System.Drawing.Size(79, 20);
            this.btInsertPoint.Text = "Insert Point";
            this.btInsertPoint.Click += new System.EventHandler(this.btInsertPoint_Click);
            // 
            // paddingToolStripMenuItem
            // 
            this.paddingToolStripMenuItem.Name = "paddingToolStripMenuItem";
            this.paddingToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.paddingToolStripMenuItem.Text = "Padding";
            this.paddingToolStripMenuItem.Visible = false;
            this.paddingToolStripMenuItem.Click += new System.EventHandler(this.paddingToolStripMenuItem_Click);
            // 
            // ss
            // 
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbTrackLength});
            this.ss.Location = new System.Drawing.Point(0, 449);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(800, 22);
            this.ss.TabIndex = 1;
            this.ss.Text = "statusStrip1";
            // 
            // lbTrackLength
            // 
            this.lbTrackLength.AutoSize = false;
            this.lbTrackLength.Name = "lbTrackLength";
            this.lbTrackLength.Size = new System.Drawing.Size(130, 17);
            this.lbTrackLength.Text = "Track Length: 0m";
            // 
            // nudTrackWidth
            // 
            this.nudTrackWidth.DecimalPlaces = 1;
            this.nudTrackWidth.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudTrackWidth.Location = new System.Drawing.Point(642, 4);
            this.nudTrackWidth.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudTrackWidth.Name = "nudTrackWidth";
            this.nudTrackWidth.Size = new System.Drawing.Size(50, 20);
            this.nudTrackWidth.TabIndex = 2;
            this.nudTrackWidth.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.nudTrackWidth.ValueChanged += new System.EventHandler(this.nudTrackWidth_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(554, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Track Width (m)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(438, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tension";
            // 
            // nudTension
            // 
            this.nudTension.DecimalPlaces = 1;
            this.nudTension.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudTension.Location = new System.Drawing.Point(489, 4);
            this.nudTension.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTension.Name = "nudTension";
            this.nudTension.Size = new System.Drawing.Size(50, 20);
            this.nudTension.TabIndex = 4;
            this.nudTension.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.nudTension.ValueChanged += new System.EventHandler(this.nudTension_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(710, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Adhesion";
            // 
            // nudAdhesion
            // 
            this.nudAdhesion.DecimalPlaces = 1;
            this.nudAdhesion.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudAdhesion.Location = new System.Drawing.Point(761, 4);
            this.nudAdhesion.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudAdhesion.Name = "nudAdhesion";
            this.nudAdhesion.Size = new System.Drawing.Size(50, 20);
            this.nudAdhesion.TabIndex = 6;
            this.nudAdhesion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAdhesion.ValueChanged += new System.EventHandler(this.nudAdhesion_ValueChanged);
            // 
            // TrackEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 471);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudAdhesion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudTension);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudTrackWidth);
            this.Controls.Add(this.ss);
            this.Controls.Add(this.mm);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mm;
            this.Name = "TrackEditor";
            this.Text = "Track Editor";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrackEditor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrackEditor_MouseMove);
            this.mm.ResumeLayout(false);
            this.mm.PerformLayout();
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrackWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTension)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdhesion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mm;
        private System.Windows.Forms.ToolStripMenuItem trackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btOpen;
        private System.Windows.Forms.ToolStripMenuItem btSave;
        private System.Windows.Forms.ToolStripMenuItem btNew;
        private System.Windows.Forms.ToolStripMenuItem btDraw;
        private System.Windows.Forms.StatusStrip ss;
        private System.Windows.Forms.ToolStripStatusLabel lbTrackLength;
        private System.Windows.Forms.NumericUpDown nudTrackWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem btEditTrack;
        private System.Windows.Forms.ToolStripMenuItem btDeletePoint;
        private System.Windows.Forms.ToolStripMenuItem btInsertPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudTension;
        private System.Windows.Forms.ToolStripMenuItem paddingToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudAdhesion;
    }
}

