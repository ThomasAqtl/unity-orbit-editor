using System;
using UnityEditor;
using UnityEngine;

namespace Components
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class FollowPath : MonoBehaviour
    {
        public Vector3[] path;
        public float period;

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
            return _index + 1 < path.Length ? path[_index + 1] : path[0];
        }
        
        private Vector3 GetPreviousPoint()
        {
            return path[_index];
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