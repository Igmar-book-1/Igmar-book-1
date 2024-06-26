using System.Collections.Generic;
using UnityEngine;

//TP2 - Agustin Picchio
namespace States
{
    public class Patrol : State
    {
        public List<Transform> waypoints;
        public int stoppingDistance;
        private int _index;
        public override void Execute(Transform target)
        {
            base.Execute(target);
            //Debug.Log("Patrol");
            if (IndexIsOutOfBounds())
            {
                ResetIndex();

            }
            PatrolWaypoints();
            _anim.SetTrigger("Patrol");
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

        private bool IndexIsOutOfBounds() 
        {
            return _index > waypoints.Count - 1;
        } 

        private bool IsCloseToWaypoint()
        {
            return Vector3.Distance(transform.position, waypoints[_index].position) <= stoppingDistance;
        }
        
    }
}