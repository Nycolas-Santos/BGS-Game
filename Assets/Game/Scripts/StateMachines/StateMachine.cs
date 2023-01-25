using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.StateMachines
{
    public class StateMachine : MonoBehaviour
    {
        public MonoBehaviour Owner { get; set; }
        public State CurrentState { get; set; }
        public List<State> States { get; set; } = new();
        
        public T AddState<T>(params object[] stateMachine) where T : State
        {
            var state = (T)Activator.CreateInstance(typeof(T), stateMachine);
            States.Add(state);
            return state;
        }
        // Returns the GameObject of this MonoBehaviour
        // Calls for the State instance Start Method
        public virtual void Start()
        {
            if (GetInitialState() != null)
            {
                CurrentState = GetInitialState();
                ChangeState(CurrentState);
            }
        }

        // Calls for the State instance Update Method
        private void Update()
        {
            if (CurrentState != null) CurrentState.Update();
        }
        // Calls for the State instance FixedUpdate Method
        private void FixedUpdate()
        {
            if (CurrentState != null) CurrentState.FixedUpdate();
        }
        // Calls for the State instance LateUpdate Method
        private void LateUpdate()
        {
            if (CurrentState != null) CurrentState.LateUpdate();
        }
        /// <summary>
        /// Changes the current state of the state machine by direct reference
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(State state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }
        /// <summary>
        /// Changes the current state of the state machine by string reflection
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(string state)
        {
            CurrentState.Exit();
            CurrentState = (State)this.GetType().GetProperty(state)?.GetValue(this);
            CurrentState.Enter();
        }
        
        /// <summary>
        /// Returns the initial state of this state machine, this method must be overridden on all state machines.
        /// </summary>
        /// <returns></returns>
        protected virtual State GetInitialState()
        {
            return null;
        }
    }
}
