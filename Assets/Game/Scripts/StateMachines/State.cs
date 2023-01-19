namespace Game.Scripts.StateMachines
{
   public class State
   {
      public string Name { get; set; }
      protected StateMachine StateMachine;

      public State(string name, StateMachine stateMachine)
      {
         this.Name = name;
         this.StateMachine = stateMachine;
      }

      public virtual void Start()
      {
         
      }
   
      public virtual void Enter()
      {
      
      }

      public virtual void Update()
      {
      
      }

      public virtual void FixedUpdate()
      {
      
      }

      public virtual void LateUpdate()
      {
      
      }

      public virtual void Exit()
      {
      
      }
   }
}
