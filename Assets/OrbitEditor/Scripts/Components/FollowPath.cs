using UnityEditor;
using UnityEngine;

namespace OrbitEditor.Scripts.Components
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class FollowPath : MonoBehaviour
    {
        [Tooltip("The set of points describing the path to follow.")]
        public Vector3[] path;

        [Tooltip("The time it takes in second for the path to be completed once.")]
        public float period;

        public bool clockwise;

        private int _index;

        public void LateUpdate()
        {
            if (ValidateData()) UpdatePosition();
        }

        private void UpdatePosition()
        {
            var resolution = path.Length;
            var time = Time.time * resolution / period % resolution;
            _index = (int)Mathf.Floor(time);
            var lerp = time - _index;
            var last = GetPreviousPoint();
            var next = GetNextPoint();
            transform.position = Vector3.Lerp(last, next, lerp);
        }

        private Vector3 GetNextPoint()
        {
            if (clockwise) return _index + 1 < path.Length ? path[_index + 1] : path[0];
            else return path.Length > _index ? path[^_index] : path[^1];
        }

        private Vector3 GetPreviousPoint()
        {
            if (clockwise) return path[_index];
            else return path.Length > _index ? path[path.Length - _index - 1] : path[0]; 
        }

        private bool ValidateData()
        {
            return path.Length >= 2 && period != 0f;
        }

        private void OnDrawGizmos()
        {
            if (ValidateData())
            {
                Handles.DrawAAPolyLine(path);
                Handles.DrawAAPolyLine(path[^1], path[0]);
                Handles.SphereHandleCap(0, transform.position, Quaternion.identity, 0.3f, EventType.Repaint);
            }
        }
    }
}