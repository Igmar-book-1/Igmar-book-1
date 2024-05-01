using UnityEngine;

namespace States
{
    public class Chase : State
    {
        public Transform target;
        public override void Execute()
        {
            base.Execute();
            Debug.Log("Chase");
            transform.LookAt(target);
            Agent.SetDestination(target.position);
        }
    }
}