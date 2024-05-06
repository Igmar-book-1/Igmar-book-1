using UnityEngine;

namespace States
{
    public class Attack : State
    {
        public override void Execute()
        {
            base.Execute();
            Debug.Log("Attack");
        }
    }
}