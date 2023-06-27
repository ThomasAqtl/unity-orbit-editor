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
        [SerializeField] private bool clockwise;

        public Orbit()
        {
            Trajectory = new Ellipsis();
            Period = 10f;
            TidalLocking = true;
            Clockwise = true;
        }
        
        public Orbit(Ellipsis trajectory, float period, bool tidalLocking, bool clockwise)
        {
            Trajectory = trajectory;
            Period = period;
            TidalLocking = tidalLocking;
            Clockwise = clockwise;
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

        public bool Clockwise
        {
            get => clockwise;
            set => clockwise = value;
        }
    }
}
