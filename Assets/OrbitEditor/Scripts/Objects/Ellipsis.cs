using System;
using UnityEngine;

namespace OrbitEditor.Scripts.Objects
{
    [Serializable]
    public class Ellipsis
    {
        public readonly float TwoPI = 6.28318530f;
        
        [SerializeField] private Vector3 center;
        [SerializeField] private Vector3 angle;
        [SerializeField] private float semiMinorAxis;
        [SerializeField] private float semiMajorAxis;
        [SerializeField] private int resolution;

        public Ellipsis()
        {
            Center = Vector3.zero;
            Angle = Vector3.zero;
            SemiMinorAxis = 5f;
            SemiMajorAxis = 10f;
            Resolution = 100;
        }

        public Ellipsis(Vector3 center, Vector3 angle, float semiMinorAxis, float semiMajorAxis, int resolution)
        {
            Center = center;
            Angle = angle;
            SemiMinorAxis = semiMinorAxis;
            SemiMajorAxis = semiMajorAxis;
            Resolution = resolution;
        }
        
        public Vector3[] ComputePoints()
        {
            var result = new Vector3[Resolution];
            var f = TwoPI / Resolution;
            var xAngle = Mathf.Deg2Rad * Angle.x;
            var zAngle = Mathf.Deg2Rad * Angle.z;
            for (var i = 0; i < Resolution; i++)
            {
                var delta = Resolution - i;
                var x = SemiMajorAxis * Mathf.Cos(f * delta) * Mathf.Cos(zAngle);
                var y = SemiMinorAxis * Mathf.Sin(xAngle) * Mathf.Sin(f * i) +
                        SemiMajorAxis * Mathf.Sin(zAngle) * Mathf.Cos(f * i);
                var z = SemiMinorAxis * Mathf.Sin(f * delta) * Mathf.Cos(xAngle);
                result[i] = Center + new Vector3(x, y, z);
            }
            return result;
        }

        public Vector3 Center
        {
            get => center;
            set => center = value;
        }

        public Vector3 Angle
        {
            get => angle;
            set => angle = value;
        }

        public float SemiMinorAxis
        {
            get => semiMinorAxis;
            set
            {
                if (value < 0)
                {
                    Debug.LogWarning($"Cannot set ellipsis' semi minor axis to {value}, it must be positive.");
                }
                semiMinorAxis = Mathf.Abs(value);   
            }
        }

        public float SemiMajorAxis
        {
            get => semiMajorAxis;
            set
            {
                if (value < 0)
                {
                    Debug.LogWarning($"Cannot set ellipsis' semi major axis to {value}, it must be positive.");
                }
                semiMajorAxis = Mathf.Abs(value);
            }
        }

        public int Resolution
        {
            get => resolution;
            set
            {
                if (value <= 1)
                {
                    Debug.LogWarning($"Cannot set ellipsis resolution to {value}, the minimum is 2");
                }
                resolution = Mathf.Max(2, value);   
            }
        }

    }
}