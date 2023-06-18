using System;

namespace OrbitEditor.Scripts.Objects
{
    [Serializable]
    public class Orbit
    {
        public Ellipsis trajectory;
        public float period;
        public bool lookAtCenter;

        public Orbit()
        {
            trajectory = new Ellipsis();
            period = 10f;
            lookAtCenter = true;
        }
        
        public Orbit(Ellipsis trajectory, float period, bool lookAtCenter)
        {
            this.trajectory = trajectory;
            this.period = period;
            this.lookAtCenter = lookAtCenter;
        }
    }
}
