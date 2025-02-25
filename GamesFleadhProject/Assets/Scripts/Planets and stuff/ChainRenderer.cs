using System.Collections.Generic;
using UnityEngine;

namespace Planets_and_stuff
{
    public class ChainRenderer : MonoBehaviour
    {
        private readonly Dictionary<DistanceJoint2D, (LineRenderer, float)> _chains = new Dictionary<DistanceJoint2D, (LineRenderer, float)>();
        public int ropeSegments = 10;  // Number of points in the rope
        public float sagAmount = 0.5f; // Adjust to control the slack
        public float breakDistance = 6f; // Maximum allowed distance before breaking

        void Start()
        {
            // Find all DistanceJoint2D components in the scene
            DistanceJoint2D[] joints = FindObjectsByType<DistanceJoint2D>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            foreach (var joint in joints)
            {
                if (joint.connectedBody != null)  // Only add valid joints
                {
                    GameObject lineObj = new GameObject("Chain");
                    LineRenderer line = lineObj.AddComponent<LineRenderer>();

                    // Set up the LineRenderer
                    line.startWidth = 0.1f;
                    line.endWidth = 0.1f;
                    line.material = new Material(Shader.Find("Sprites/Default"));
                    line.positionCount = ropeSegments;

                    float initialDistance = Vector2.Distance(joint.transform.position, joint.connectedBody.position);
                    _chains.Add(joint, (line, initialDistance));
                }
            }
        }

        void Update()
        {
            List<DistanceJoint2D> toRemove = new List<DistanceJoint2D>();

            foreach (var entry in _chains)
            {
                DistanceJoint2D joint = entry.Key;
                LineRenderer line = entry.Value.Item1;
                float originalDistance = entry.Value.Item2;

                if (!joint || !joint.connectedBody)
                {
                    toRemove.Add(joint);
                    continue;
                }

                Transform start = joint.transform;
                Transform end = joint.connectedBody.transform;
                float currentDistance = Vector2.Distance(start.position, end.position);

                // Check if the rope should break
                if (currentDistance > breakDistance)
                {
                    toRemove.Add(joint);
                    continue;
                }

                Vector3[] positions = new Vector3[ropeSegments];

                for (int i = 0; i < ropeSegments; i++)
                {
                    float t = i / (float)(ropeSegments - 1);
                    Vector3 point = Vector3.Lerp(start.position, end.position, t);

                    // Add sag effect (makes the rope "hang")
                    float sagFactor = Mathf.Sin(t * Mathf.PI) * sagAmount;
                    point.y -= sagFactor * Mathf.Max(0, originalDistance - currentDistance);

                    positions[i] = point;
                }

                line.positionCount = ropeSegments;
                line.SetPositions(positions);
            }

            // Remove broken joints
            foreach (var joint in toRemove)
            {
                if (joint)
                {
                    Destroy(_chains[joint].Item1.gameObject); // Destroy LineRenderer object
                    Destroy(joint); // Destroy the joint itself
                    _chains.Remove(joint);
                }
            }
        }
    }
}
