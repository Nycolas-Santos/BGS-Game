using System;
using System.Collections.Generic;
using Game.Scripts.StateMachines.Player.States;
using UnityEngine;

namespace Game.Scripts.StateMachines.Player
{
    public class MovementStateMachine : StateMachine
    {
        public Idle Idle { get; set; }
        public Moving Moving { get; set; }
        protected override State GetInitialState()
        {
            return Idle;
        }
        
        private void Awake()
        {
            Moving = AddState<Moving>(this);
            Idle = AddState<Idle>(this);
        }

        public override void Start()
        {
            base.Start();
            foreach (var state in States)
            {
                state.Start();
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(50,50,50,50),CurrentState.Name);
        }
    }
}
