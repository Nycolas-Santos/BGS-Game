
using System.Collections;
using UnityEngine;

namespace Game.Scripts.StateMachines.DayCycle.States
{
    public class Afternoon : State
    {
        public Afternoon(DayCycleStateMachine stateMachine) : base("Afternoon", stateMachine) { }
        
        public float timeToWait; // number of seconds to wait
        private float currentTime; // current elapsed time
        private bool functionExecuted = false; // flag to track if function has been executed
        public override void Start()
        {
            base.Start();
        }

        public override void Enter()
        {
            base.Enter();
            timeToWait = ((GameManager)StateMachine.Owner).dayCycleSettings.morningDuration;
            currentTime = 0f;
        }

        public override void Update()
        {
            base.Update();
            currentTime += Time.deltaTime; // increment current time by time since last frame
            ((GameManager)StateMachine.Owner).dayCycleSettings.morningPostProcessing.weight -= Time.deltaTime;
            ((GameManager)StateMachine.Owner).dayCycleSettings.afternoonPostProcessing.weight += Time.deltaTime;
            ((GameManager)StateMachine.Owner).dayCycleSettings.nightPostProcessing.weight -= Time.deltaTime;
            ((GameManager)StateMachine.Owner).dayCycleSettings.morningPostProcessing.weight =
                Mathf.Clamp01(((GameManager)StateMachine.Owner).dayCycleSettings.morningPostProcessing.weight);
            ((GameManager)StateMachine.Owner).dayCycleSettings.afternoonPostProcessing.weight =
                Mathf.Clamp01(((GameManager)StateMachine.Owner).dayCycleSettings.afternoonPostProcessing.weight);
            ((GameManager)StateMachine.Owner).dayCycleSettings.nightPostProcessing.weight =
                Mathf.Clamp01(((GameManager)StateMachine.Owner).dayCycleSettings.nightPostProcessing.weight);
            if (currentTime >= timeToWait && !functionExecuted) // check if time to wait has been reached and function has not yet been executed
            {
                functionExecuted = true; // set flag to indicate function has been executed
                StateMachine.ChangeState(((DayCycleStateMachine)StateMachine).Night);
            }
        }

        public override void Exit()
        {
            base.Exit();
            functionExecuted = false;
        }
    }
}
