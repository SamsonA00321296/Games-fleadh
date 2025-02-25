using System.Collections.Generic;
using UnityEngine;

namespace Gravity_and_orbit_scripts
{
    public class CircularPath : MonoBehaviour
    {
        [Header("Waypoint variables")]
        public Transform centerPoint; //center of the circular orbit
        public float radius = 5f; // radius of the orbit circle
        public int noOfWaypoints = 20; // number of waypoints, higher amount will make a smoother orbit, but might decrease performance idek
        public float speed = 5f; // speed the player moves between the waypoints
        public float rotationspeed = 200f; // speed at which the ship rotates when moving between the waypoints

        [Header("Other script variables")]
        public bool inOrbit;
    
    
        private List<Vector2> _waypoints;
        private int _currentWaypoint;
        private bool _isMoving;

        void Start()
        {
            _waypoints = new List<Vector2>();
            CalculateWaypoints();
        
        }

        void Update()
        {
            if (_isMoving)
            {
                OrbitHopefully();
            }
        
        }

        void CalculateWaypoints()
        {
            for (int i = 0; i < noOfWaypoints; i++)
            {
                float angle = i * Mathf.PI * 2f / noOfWaypoints;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                Vector2 waypoint = new Vector2(centerPoint.position.x + x, centerPoint.position.y + y);
                _waypoints.Add(waypoint);
            }
        }

        public void StartPath()
        {
            if (_waypoints.Count > 0)
            {
                inOrbit = true;
                _isMoving = true;
            }
        }

        public void StopPath()
        {
            inOrbit = false;
            _isMoving = false;
        }

        void OrbitHopefully()
        {
            if (_currentWaypoint < _waypoints.Count)
            {
                Vector2 targetWaypoint = _waypoints[_currentWaypoint];
                transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            
                Vector2 direction = (targetWaypoint - (Vector2)transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetrotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetrotation, rotationspeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, targetWaypoint) < 0.1f)
                
                {
                    _currentWaypoint++;
                    if (_currentWaypoint >= _waypoints.Count)
                    {
                        _currentWaypoint = 0;
                    }
                }
            }
        
        }
    }
}
