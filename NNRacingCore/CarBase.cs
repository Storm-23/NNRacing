using System;

namespace NNRacingCore
{
    /// <summary>
    /// Provides base physics of car
    /// </summary>
    public class CarBase
    {
        /// <summary>
        /// Масса, кг
        /// </summary>
        public float Mass = 1500;

        /// <summary>
        /// Колесная база, м
        /// </summary>
        public float Length = 2.6f;

        /// <summary>
        /// Коэфф. сцепления колес с поверхностью
        /// </summary>
        public float Adhesion = 1f;

        /// <summary>
        /// Угол на который в данный момент повернуты передние колеса
        /// (рулевое управление), радианы
        /// </summary>
        public float SteeringAngle = 0;

        /// <summary>
        /// Текущий коэфф мощности двигателя (от 0 до 1)
        /// (выжимание газа)
        /// </summary>
        public float Throttle = 0;

        /// <summary>
        /// Мжщность двигателя, уе
        /// </summary>
        public float EnginePower = 15500;

        /// <summary>
        /// Тормоз
        /// </summary>
        public bool Breaks;

        /// <summary>
        /// Текущее положение центра автомобиля
        /// </summary>
        public Vector2 Pos;

        /// <summary>
        /// Текущее направление корпуса
        /// </summary>
        public Vector2 LookAt;

        /// <summary>
        /// Текущее направление движения (скорость), м/с
        /// </summary>
        public Vector2 Velocity { get; protected set; }

        /// <summary>
        /// Скольжение?
        /// </summary>
        public bool IsSliding;

        /// <summary>
        /// Чувствительность руля
        /// </summary>
        public float SteeringSens = 5f;

        /// <summary>
        /// Трение воздуха
        /// </summary>
        public float AirFriction = 0.05f;

        /// <summary>
        /// Трение при торможении
        /// </summary>
        public float BreaksFriction = 1f;

        /// <summary>
        /// Трение повернутых передних колес
        /// </summary>
        public float TireFriction = 0.3f;

        public CarBase()
        {
            LookAt = new Vector2(0, 1);
        }

        public void Update(float throttle, float steering, bool breaks, float dt)
        {
            //max steering angle
            var maxAngle = 30 * PointFHelper.ToRadians;

            //dump steering
            var k = SteeringSens * dt;
            var s = SteeringAngle;
            s = s * (1 - k) + steering * k;
            SteeringAngle = ToDiapason(s, -maxAngle, maxAngle);
            Throttle = ToDiapason(throttle, -1, 1f);
            Breaks = breaks;

            //F = ma
            var force = Throttle * EnginePower; //сила тяги
            Velocity += LookAt * (force * dt / Mass);

            //air friction
            Velocity -= Velocity * (AirFriction * dt);

            //tires friction
            var friction = TireFriction * Math.Abs(SteeringAngle); //трение повернутых шин

            //breaks
            if (Breaks)
                friction = BreaksFriction;

            Velocity -= Velocity.Projection(LookAt) * (friction * Adhesion * dt);

            //wheels position
            var frontWheel = Pos + LookAt * (Length / 2);
            var backWheel = Pos - LookAt * (Length / 2);

            backWheel = CalcWheelMoving(backWheel, LookAt, Velocity, dt);
            frontWheel = CalcWheelMoving(frontWheel, LookAt.Rotate(SteeringAngle), Velocity, dt);

            //new car orientation
            LookAt = (frontWheel - backWheel).Normalized();

            //calc new velocity
            var speed = Velocity.Length();

            var prev = Velocity;
            Velocity = LookAt * speed;
            Velocity = prev.MoveTowards(Velocity, 0.5f * Adhesion);

            //assign new pos
            Pos = Pos.Add(Velocity * dt);
        }

        float ToDiapason(float val, float min, float max)
        {
            if (val < min) return min;
            if (val > max) return max;
            return val;
        }

        private Vector2 CalcWheelMoving(Vector2 wheelPos, Vector2 wheelDir, Vector2 velocity, float dt)
        {
            //раскладываем скорости вдоль и поперек колеса
            var Vt = velocity.Projection(wheelDir); //tangent component
            var Vn = velocity.Sub(Vt); //normal component

            //смещение колеса
            var moving = (Vt + Velocity) * 0.5f * dt; //с учетом скольжения
            //var moving = Vt * dt; //без учета скольжения

            return wheelPos.Add(moving);
        }
    }
}