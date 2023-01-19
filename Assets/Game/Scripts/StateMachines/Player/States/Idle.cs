using UnityEngine;

namespace Game.Scripts.StateMachines.Player.States
{
    public class Idle : State
    {
        public Idle(MovementStateMachine stateMachine) : base("Idle", stateMachine) { }
        

        public override void Start()
        {
            base.Start();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            var inputHorizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")); // Storing Input Values
            var inputVertical = Mathf.Abs(Input.GetAxisRaw("Vertical")); // Storing Input Values
            if (inputHorizontal > Mathf.Epsilon || inputVertical > Mathf.Epsilon) // Checking if there is a significant input value
            {
                StateMachine.ChangeState(((MovementStateMachine)StateMachine).Moving); // Change state to MOVING if there is input
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
