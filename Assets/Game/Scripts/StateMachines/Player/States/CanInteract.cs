using Game.Scripts.Interactables;
using UnityEngine;

namespace Game.Scripts.StateMachines.Player.States
{
    public class CanInteract : State
    {
        public CanInteract(InteractionStateMachine stateMachine) : base("CanInteract", stateMachine) { }

        private const float INTERACT_RADIUS = 2f;
        private float interactRadius;
        private IInteractable nearestInteractable;
        private LayerMask interactLayer;
        private Transform playerTransform;
        

        public override void Start()
        {
            base.Start();
            if (StateMachine.Owner != null)
            {
                interactRadius = ((Scripts.Player)StateMachine.Owner).interactRadius;
                interactLayer = ((Scripts.Player)StateMachine.Owner).interactLayer;
                playerTransform = ((Scripts.Player)StateMachine.Owner).transform;
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            CheckForInteractables();
            if (Input.GetKeyDown(KeyCode.E) && nearestInteractable != null)
            {
                ((InteractionStateMachine)StateMachine).CurrentInteractable = nearestInteractable;
                StateMachine.ChangeState(((InteractionStateMachine)StateMachine).Interacting);
            }
        }
        private void CheckForInteractables()
        {
            // Use OverlapCircleNonAlloc to check for objects that inherit from IInteractable within interactionRadius
        Collider2D[] interactablesInRadius = Physics2D.OverlapCircleAll(playerTransform.position, 2f);

        // Set closestInteractable to null initially
        nearestInteractable = null;
        float closestDistance = float.MaxValue;

        // Iterate through the interactablesInRadius array
        for (int i = 0; i < interactablesInRadius.Length; i++)
        {
            // Check if the current object in the array inherits from IInteractable
            if (interactablesInRadius[i].GetComponent<IInteractable>() != null)
            {
                // Calculate the distance between the player and the current object
                float distance = Vector2.Distance(playerTransform.position, interactablesInRadius[i].transform.position);

                // Check if the distance is less than minInteractionDistance
                if (distance < interactRadius)
                {
                    // Check if the current object is closer than the closestInteractable
                    if (distance < closestDistance)
                    {
                        // Set closestInteractable to the current object and update closestDistance
                        nearestInteractable = interactablesInRadius[i].GetComponent<IInteractable>();
                        closestDistance = distance;
                    }
                }
                else
                {
                    // Set PlayerCanInteract to false for objects that are not within minInteractionDistance
                    interactablesInRadius[i].GetComponent<IInteractable>().PlayerCanInteract = false;
                }
            }
        }

        // Check if closestInteractable is not null
        if (nearestInteractable != null)
        {
            // Set PlayerCanInteract to true for closestInteractable
            nearestInteractable.PlayerCanInteract = true;
        }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
