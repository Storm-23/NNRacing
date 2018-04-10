namespace CarPhysicsTester
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudMass = new System.Windows.Forms.NumericUpDown();
            this.nudAdhesion = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudPower = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btStart = new System.Windows.Forms.Button();
            this.pnTrack = new CarPhysicsTester.TrackPanel();
            ((System.ComponentModel.ISupportInitialize)(this.nudMass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdhesion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Масса, кг";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudMass
            // 
            this.nudMass.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudMass.Location = new System.Drawing.Point(136, 9);
            this.nudMass.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMass.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudMass.Name = "nudMass";
            this.nudMass.Size = new System.Drawing.Size(71, 20);
            this.nudMass.TabIndex = 2;
            this.nudMass.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // nudAdhesion
            // 
            this.nudAdhesion.DecimalPlaces = 1;
            this.nudAdhesion.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudAdhesion.Location = new System.Drawing.Point(136, 43);
            this.nudAdhesion.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudAdhesion.Name = "nudAdhesion";
            this.nudAdhesion.Size = new System.Drawing.Size(71, 20);
            this.nudAdhesion.TabIndex = 8;
            this.nudAdhesion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 33);
            this.label4.TabIndex = 7;
            this.label4.Text = "Коэфф сцепления колес с поверхн.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // nudPower
            // 
            this.nudPower.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPower.Location = new System.Drawing.Point(349, 11);
            this.nudPower.Maximum = new decimal(new int[] {
            25000,
            0,
            0,
            0});
            this.nudPower.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudPower.Name = "nudPower";
            this.nudPower.Size = new System.Drawing.Size(71, 20);
            this.nudPower.TabIndex = 10;
            this.nudPower.Value = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(196, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 29);
            this.label5.TabIndex = 9;
            this.label5.Text = "Мощность двигателя (уе)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(476, 17);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 46);
            this.btStart.TabIndex = 14;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // pnTrack
            // 
            this.pnTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnTrack.BackColor = System.Drawing.Color.White;
            this.pnTrack.Location = new System.Drawing.Point(12, 76);
            this.pnTrack.Name = "pnTrack";
            this.pnTrack.Size = new System.Drawing.Size(663, 432);
            this.pnTrack.TabIndex = 13;
            this.pnTrack.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 520);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.pnTrack);
            this.Controls.Add(this.nudPower);
            this.Controls.Add(this.nudAdhesion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudMass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Car Simulation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nudMass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdhesion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMass;
        private System.Windows.Forms.NumericUpDown nudAdhesion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudPower;
        private System.Windows.Forms.Label label5;
        private TrackPanel pnTrack;
        private System.Windows.Forms.Button btStart;
    }
}

