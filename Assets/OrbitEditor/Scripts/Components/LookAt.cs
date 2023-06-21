using System;
using UnityEditor;
using UnityEngine;

namespace OrbitEditor.Scripts.Components
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class LookAt : MonoBehaviour
    {
        [Tooltip("The transform to look at.")] public Transform target;

        private bool _validTarget;

        private void LateUpdate()
        {
            try
            {
                if (_validTarget) transform.LookAt(target);
            }
            catch (NullReferenceException)
            {
                _validTarget = false;
                throw;
            }
        }

        public void SetTarget(Transform newTarget)
        {
            _validTarget = newTarget != null;
            target = newTarget;
        }

        private void Reset()
        {
            _validTarget = false;
        }

        private void OnValidate()
        {
            _validTarget = target != null;
        }

        private void OnDisable()
        {
            transform.rotation = Quaternion.identity;
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.blue;
            Handles.DrawAAPolyLine(transform.position, transform.position + transform.forward);
        }
    }
}