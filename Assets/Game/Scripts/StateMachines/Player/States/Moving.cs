using UnityEngine;

namespace Game.Scripts.StateMachines.Player.States
{
    public class Moving : State
    {
        public Moving(MovementStateMachine stateMachine) : base("Moving", stateMachine) { }
        
        private Rigidbody2D rigidbody;
        private float speed;
        private Vector2 movement;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        
        public override void Start()
        {
            base.Start();
            Debug.Log("Start");
            if (StateMachine.Owner != null)
            {
                rigidbody = ((Scripts.Player)StateMachine.Owner).Rigidbody;
                speed = ((Scripts.Player)StateMachine.Owner).speed;
                animator = ((Scripts.Player)StateMachine.Owner).Animator;
                spriteRenderer = ((Scripts.Player)StateMachine.Owner).SpriteRenderer;
            }
        }

        public override void Enter()
        {
            base.Enter();
            animator.SetBool(IsWalking,true);
        }

        public override void Update()
        {
            base.Update();
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (movement.x <= -0.001f) spriteRenderer.flipX = true;
            else if (movement.x >= 0.001f) spriteRenderer.flipX = false; // Changes the Player direction based on the X movement
            if (Mathf.Abs(movement.x) < Mathf.Epsilon && Mathf.Abs(movement.y) < Mathf.Epsilon) // Checking if there is no significant input value
            {
                StateMachine.ChangeState(((MovementStateMachine)StateMachine).Idle); // Change state to IDLE if there is no input
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            rigidbody.MovePosition(rigidbody.position + movement.normalized * speed * Time.fixedDeltaTime);
        }

        public override void Exit()
        {
            base.Exit();
            animator.SetBool(IsWalking,false);
        }
    }
}
