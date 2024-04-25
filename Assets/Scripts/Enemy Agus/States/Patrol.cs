﻿using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class Patrol : State
    {
        public List<Transform> waypoints;
        public int stoppingDistance;
        private int _index;
        public override void Execute()
        {
            base.Execute();
            Debug.Log("Patrol");
            ResetIndex();
            PatrolWaypoints();
        }
    
        private void ResetIndex()
        {
            _index = 0;
        }

        private void PatrolWaypoints()
        {
            Agent.SetDestination(waypoints[_index].position);
        }

        private void FixedUpdate()
        {
            if (IsCloseToWaypoint())
            {
                _index++;
            
                if (IndexIsOutOfBounds())
                    _index = 0;
            
                PatrolWaypoints();
            }
        }

        private bool IndexIsOutOfBounds() => _index > waypoints.Count -1;

        private bool IsCloseToWaypoint() =>
            Vector3.Distance(transform.position, waypoints[_index].position) <= stoppingDistance;
    }
}