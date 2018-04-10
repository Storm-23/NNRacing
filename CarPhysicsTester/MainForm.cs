using System;
using System.Windows.Forms;
using NNRacingCore;

namespace CarPhysicsTester
{
    public partial class MainForm : Form
    {
        private CarBase car;

        public MainForm()
        {
            InitializeComponent();
            Application.Idle += (o,O)=>Update();
        }

        private float wheelAngle = 0;
        private float throttle = 0;
        private float breaks = 0;

        void Update()
        {
            if (car == null) return;

            var a = 40 * (float)Math.PI / 180;

            if (Keyboard.IsKeyDown(Keys.Left)) wheelAngle = +a;
            else
            if (Keyboard.IsKeyDown(Keys.Right)) wheelAngle = -a;
            else
                wheelAngle = 0;

            if (Keyboard.IsKeyDown(Keys.Up))
                throttle += 0.2f;
            else
            if (Keyboard.IsKeyDown(Keys.Down))
                throttle -= 0.2f;
            else
                throttle = 0;

            var breaks = Keyboard.IsKeyDown(Keys.Space);

            throttle = Math.Max(-1, Math.Min(1, throttle));

            car.Update(throttle, wheelAngle, breaks, 0.01f);
            pnTrack.Invalidate();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            pnTrack.Focus();
            ActiveControl = pnTrack;
            Init();
        }

        private void Init()
        {
            car = new CarBase();
            pnTrack.Car = car;
            wheelAngle = 0;
            throttle = 0;
            car.Mass = (float)nudMass.Value;
            car.EnginePower = (float)nudPower.Value;
            car.Adhesion = (float)nudAdhesion.Value;
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
    }
}
