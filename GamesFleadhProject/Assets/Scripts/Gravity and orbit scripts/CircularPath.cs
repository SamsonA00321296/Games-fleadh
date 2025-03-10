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
        public float speed = 5f; // speed the player moves between the waypoints, i have this set as 2 on my end
        public float rotationspeed = 200f; // speed at which the ship rotates when moving between the waypoints, its set as 200 here but id reccomend something between 20-25

        [Header("Other script variables")]
        public bool inOrbit; // used in another script for something i think its the player movement one
    
    
        private List<Vector2> _waypoints; // the list of waypoints the player will travel through
        private int _currentWaypoint; // keeps track of the current waypoint
        private bool _isMoving;// bool to state wether the player is in orbit around thier planet or not
        

        void Start()
        {
            // initialises the waypoitns list and calls the method to calculate the positions of the waypoints
            _waypoints = new List<Vector2>();
            CalculateWaypoints();
        
        }

        void Update()
        {
            if (_isMoving)
            {
                // calls the orbit method, this is the one that moves the player
                OrbitHopefully();
            }
        
        }

        void CalculateWaypoints()
        {
            // for loop that runs once for every waypoint you want to have
            for (int i = 0; i < noOfWaypoints; i++)
            {
                
                // this just sets up the waypoints, lots of mathy crap, just sets them up in a circular pattern distance depending on the amount of waypoints you have and the radius of the circle
                float angle = i * Mathf.PI * 2f / noOfWaypoints;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                Vector2 waypoint = new Vector2(centerPoint.position.x + x, centerPoint.position.y + y);
                // then adds them to the list you made earlier
                _waypoints.Add(waypoint);
            }
        }

        // call this method to start the orbit, turns both needed variabled to true
        public void StartPath()
        {
            if (_waypoints.Count > 0)
            {
                inOrbit = true;
                _isMoving = true;
            }
        }

        // you get the idea, call this one to stop the orbit
        public void StopPath()
        {
            inOrbit = false;
            _isMoving = false;
        }

        void OrbitHopefully()
        {
            if (_currentWaypoint < _waypoints.Count)
            {
                // makes the current waypoint in the list, the target, and then move the players transform towards the next point
                Vector2 targetWaypoint = _waypoints[_currentWaypoint];
                transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            
                // TLDR of below, its to rotate the player while in orbit so you dont have a really crappy looking point to point thing rather than a smooth orbit
                
                // sets up the direction the player is supposed to move in
                Vector2 direction = (targetWaypoint - (Vector2)transform.position).normalized;
                //this is then used here, as an angle from one waypoint to the next is calculated
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // that angle is then used here, where we calculate the point of rotation we want to be at by the time we have reached the next waypoint
                Quaternion targetrotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                // FINALLY we can rotate the poxy ship gradually towards the next waypoint while moving.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetrotation, rotationspeed * Time.deltaTime);

                // this basically moves on to the next waypoints once you reach the target, and resets the current wayponit back to zero if youve done a full good lap
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
