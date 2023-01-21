using Game.Scripts.Interactables;
using Game.Scripts.StateMachines.Player.States;
using UnityEngine;

namespace Game.Scripts.StateMachines.Player
{
    public class InteractionStateMachine : StateMachine
    {
        public CanInteract CanInteract { get; set; }
        public Interacting Interacting { get; set; }
        public CanNotInteract CanNotInteract { get; set; }

        public IInteractable CurrentInteractable { get; set; }

        protected override State GetInitialState()
        {
            return CanInteract;
        }
        
        private void Awake()
        {
            CanInteract = AddState<CanInteract>(this);
            Interacting = AddState<Interacting>(this);
            CanNotInteract = AddState<CanNotInteract>(this);
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
            GUI.Label(new Rect(50,25,100,50),CurrentState.Name);
        }
    }
}
