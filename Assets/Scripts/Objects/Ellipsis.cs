using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    [Serializable]
    public class Ellipsis
    {
        public readonly float TwoPI = 2 * Mathf.PI;
        
        public Vector3 center;
        public Vector3 angle;
        public float semiMinorAxis;
        public float semiMajorAxis;
        public int resolution;

        public Ellipsis()
        {
            center = Vector3.zero;
            angle = Vector3.zero;
            semiMinorAxis = 5f;
            semiMajorAxis = 10f;
            resolution = 100;
        }

        public Ellipsis(Vector3 center, Vector3 angle, float semiMinorAxis, float semiMajorAxis, int resolution)
        {
            this.center = center;
            this.angle = angle;
            this.semiMinorAxis = semiMinorAxis;
            this.semiMajorAxis = semiMajorAxis;
            this.resolution = resolution;
        }

        public Vector3[] GetTrajectory()
        {
            var result = new Vector3[resolution];
            var f = 2 * Mathf.PI / resolution;
            var xAngle = Mathf.Deg2Rad * angle.x;
            var zAngle = Mathf.Deg2Rad * angle.z;
            for (var i = 0; i < resolution; i++)
            {
                var delta = resolution - i;
                var x = semiMajorAxis * Mathf.Cos(f * delta) * Mathf.Cos(zAngle);
                var y = semiMinorAxis * Mathf.Sin(xAngle) * Mathf.Sin(f * i) +
                        semiMajorAxis * Mathf.Sin(zAngle) * Mathf.Cos(f * i);
                var z = semiMinorAxis * Mathf.Sin(f * delta) * Mathf.Cos(xAngle);
                result[i] = center + new Vector3(x, y, z);
            }
            return result;
        }
    }
}