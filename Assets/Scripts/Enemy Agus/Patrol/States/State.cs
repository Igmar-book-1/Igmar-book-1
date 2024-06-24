using LineOfSight;
using UnityEngine;
using UnityEngine.AI;

//TP2 - Agustin Picchio
namespace States
{
    public abstract class State : MonoBehaviour
    {
        protected NavMeshAgent Agent;
        protected LineOfSightBehaviour _lineOfSight;
        public float lineOfSightRange;
        public float lineOfSightAngle;
        public float speed; 
        protected Animator _anim;
        

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            _lineOfSight = GetComponent<LineOfSightBehaviour>();
            _anim = GetComponent<Animator>();
            
        }

        public virtual void Execute(Transform target)
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