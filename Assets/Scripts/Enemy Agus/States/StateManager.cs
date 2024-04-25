using UnityEngine;

namespace States
{
    public class StateManager : MonoBehaviour
    {
        public State attackState; 
        public State chaseState; 
        public State patrolState;
        private State _currentState;
        public Transform target;
        void Start()
        {
            SetInitialState(patrolState);
        }

        private void SetInitialState(State state)
        {
            _currentState = state;
            _currentState.Execute();
        }

        void Update()
        {
            if (IsTargetVisible())
            {
                if (IsTargetClose())
                {
                    SetCurrentState(attackState);
                    return;
                }
                SetCurrentState(chaseState);
            }
            else
            {
                SetCurrentState(patrolState);
            }

            HandleCheatClicked();
        }

        private void SetCurrentState(State state)
        {
            if(IsInTheSameState(state)) return;
            _currentState = state;
            _currentState.Execute();
        }
    
        private bool IsInTheSameState(State state) => _currentState == state;

        private bool IsTargetClose() => Vector3.Distance(transform.position, target.position) < _currentState.GetLineOfSight().range / 2;

        private bool IsTargetVisible() => _currentState.GetLineOfSight().IsInSight(target);

        private void HandleCheatClicked()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                patrolState.Execute();
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                chaseState.Execute();
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                attackState.Execute();
            }
        }
    }
}