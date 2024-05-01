using LineOfSight;
using UnityEngine;
using UnityEngine.AI;

namespace States
{
    public abstract class State : MonoBehaviour
    {
        protected NavMeshAgent Agent;
        private LineOfSightBehaviour _lineOfSight;
        public float lineOfSightRange;
        public float lineOfSightAngle;
        public float speed;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            _lineOfSight = GetComponent<LineOfSightBehaviour>();
        }

        public virtual void Execute()
        {
            SetupLineOfSight();
            SetupSpeed();
        }

        private void SetupSpeed()
        {
            Agent.speed = speed;
        }

        private void SetupLineOfSight()
        {
           _lineOfSight.range = lineOfSightRange;
           _lineOfSight.angle = lineOfSightAngle;
        }
        public LineOfSightBehaviour GetLineOfSight() 
        { 
            return _lineOfSight;
        }
    } 
}