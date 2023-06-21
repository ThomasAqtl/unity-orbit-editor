using System;
using UnityEngine;

namespace OrbitEditor.Scripts.Objects
{
    [Serializable]
    public class Orbit
    {
        [SerializeField] private Ellipsis trajectory;
        [SerializeField] private float period;
        [SerializeField] private bool tidalLocking;

        public Orbit()
        {
            Trajectory = new Ellipsis();
            Period = 10f;
            TidalLocking = true;
        }
        
        public Orbit(Ellipsis trajectory, float period, bool tidalLocking)
        {
            Trajectory = trajectory;
            Period = period;
            TidalLocking = tidalLocking;
        }

        public Ellipsis Trajectory
        {
            get => trajectory;
            set => trajectory = value;
        }

        public float Period
        {
            get => period;
            set => period = value;
        }

        public bool TidalLocking
        {
            get => tidalLocking;
            set => tidalLocking = value;
        }
    }
}
