using Game.Scripts.StateMachines.DayCycle.States;
using UnityEngine;

namespace Game.Scripts.StateMachines.DayCycle
{
    public class DayCycleStateMachine : StateMachine
    {
        public Morning Morning { get; set; }
        public Afternoon Afternoon { get; set; }
        public Night Night { get; set; }

        protected override State GetInitialState()
        {
            return Morning;
        }
        
        private void Awake()
        {
            Morning = AddState<Morning>(this);
            Afternoon = AddState<Afternoon>(this);
            Night = AddState<Night>(this);
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
            GUI.Label(new Rect(50,75,100,50),CurrentState.Name);
        }
    }
}
