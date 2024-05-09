using UnityEngine;
using System.Collections;

namespace States
{
    public class Attack : State
    {
        private bool isCalled;
        StateManager stateManager;
        private bool isAttackAllowed;
        private string onAttack = "onAttack";

        public override void Execute(Transform target)
        {
            base.Execute(target);
            Debug.Log("Attack");
            transform.LookAt(target);
            Agent.SetDestination(target.position);
            MakeAttack();
            

        }
        void MakeAttack()
        {
            isAttackAllowed = false;
            _anim.SetTrigger(onAttack);
            Debug.Log("Ataque");
            StartCoroutine(AttackFreeze());

        }

        IEnumerator AttackFreeze()
        {

            yield return new WaitForSeconds(5f);
            isAttackAllowed = true;
        }
    }
}