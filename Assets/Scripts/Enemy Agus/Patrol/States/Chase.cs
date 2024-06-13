using UnityEngine;

namespace States
{
    public class Chase : State
    {
        
        public override void Execute(Transform target)
        {
            base.Execute(target);
            Debug.Log("Chase");
            transform.LookAt(target);
            Agent.SetDestination(target.position);
            _anim.SetTrigger("Chase");
            
        }
    }
}