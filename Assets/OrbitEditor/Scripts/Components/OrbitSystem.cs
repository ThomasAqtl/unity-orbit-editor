using System.Collections.Generic;
using OrbitEditor.Scripts.Components;
using OrbitEditor.Scripts.Objects;
using UnityEditor;
using UnityEngine;
using OrbitSystem = OrbitEditor.Scripts.Components.OrbitSystem;

namespace OrbitEditor.Scripts.Components
{
    public class OrbitSystem : MonoBehaviour
    {
        public List<Orbit> orbits;
        private List<Transform> _orbitsTransforms;

        [MenuItem("GameObject/Orbit system/New orbit system")]
        public static void CreateOrbitSystem()
        {
            var root = Selection.activeTransform;
            var orbitSystemObj = new GameObject
            {
                name = "Orbit system",
                transform = { parent = root, position = Vector3.zero, rotation = Quaternion.identity }
            };
            orbitSystemObj.AddComponent<OrbitSystem>();
        }
        
        private void Reset()
        {
            DestroyAllOrbits();
            AddDefaultOrbit();
        }
        
        public void DestroyAllOrbits()
        {
            if (_orbitsTransforms == null) return;
            
            for (var i = _orbitsTransforms.Count - 1; i >= 0; i--)
            {
                if (Application.isPlaying) Destroy(_orbitsTransforms[i].gameObject);
                else DestroyImmediate(_orbitsTransforms[i].gameObject);
            }

            _orbitsTransforms = new List<Transform>();
        }

        public void AddDefaultOrbit()
        {
            if (orbits == null) orbits = new List<Orbit>();
            orbits.Add(new Orbit());
            
            var orbitObject = new GameObject
            {
                name = "Orbit",
                transform = { parent = transform }
            };
            
            AddOrbitComponents(orbitObject, orbits[^1]);
            if (_orbitsTransforms == null) _orbitsTransforms = new List<Transform>();
            _orbitsTransforms.Add(orbitObject.transform);
        }


        private void AddOrbitComponents(GameObject orbitObject, Orbit orbit)
        {
            var followPath = orbitObject.AddComponent<FollowPath>();
            followPath.path = orbit.Trajectory.ComputePoints();
            followPath.period = orbit.Period;
            followPath.clockwise = orbit.Clockwise;

            var lookAt = orbitObject.AddComponent<LookAt>();
            lookAt.SetTarget(transform);
            lookAt.enabled = orbit.TidalLocking;
        }

        public void Refresh()
        {
            DestroyAllOrbits();
            _orbitsTransforms = new List<Transform>();
            foreach (var orbit in orbits)
            {
                var orbitObject = new GameObject
                {
                    name = "Orbit",
                    transform = { parent = transform }
                };
                AddOrbitComponents(orbitObject, orbit);
                _orbitsTransforms.Add(orbitObject.transform);
            }
        }
    }
}

[CustomEditor(typeof(OrbitSystem))]
public class OrbitSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var orbitSystem = (OrbitSystem)target;
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            orbitSystem.Refresh();
        }
    }
}