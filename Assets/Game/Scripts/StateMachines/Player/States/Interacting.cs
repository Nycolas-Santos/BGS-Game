using UnityEngine;

namespace Game.Scripts.StateMachines.Player.States
{
    public class Interacting : State
    {
        public Interacting(InteractionStateMachine stateMachine) : base("Interacting", stateMachine) { }

        public override void Start()
        {
            base.Start();
        }
        
        public override void Enter()
        {
            base.Enter();
            ((InteractionStateMachine)StateMachine).CurrentInteractable.OnInteractionStarted += InteractionStart;
            ((InteractionStateMachine)StateMachine).CurrentInteractable.OnInteractionFinished += InteractionFinish;
            ((InteractionStateMachine)StateMachine).CurrentInteractable.Interact();
        }

        private void InteractionStart()
        {
            if (((InteractionStateMachine)StateMachine).CurrentInteractable.LockPlayerInPlace) 
                ((Scripts.Player)StateMachine.Owner).SetMovementState("Locked");
        }
        
        private void InteractionFinish()
        {
            if (((InteractionStateMachine)StateMachine).CurrentInteractable.LockPlayerInPlace) 
                ((Scripts.Player)StateMachine.Owner).SetMovementState("Idle");
            
            StateMachine.ChangeState(((InteractionStateMachine)StateMachine).CanInteract);
        }
        
        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            ((InteractionStateMachine)StateMachine).CurrentInteractable.OnInteractionStarted -= InteractionStart;
            ((InteractionStateMachine)StateMachine).CurrentInteractable.OnInteractionFinished -= InteractionFinish;
        }
    }
}
