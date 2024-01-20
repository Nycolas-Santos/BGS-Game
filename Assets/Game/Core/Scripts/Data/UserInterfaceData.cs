using UnityEngine;

namespace Game.Core.Scripts.Data
{
   [CreateAssetMenu(fileName = "UserInterfaceData", menuName = "Custom/User Interface Data", order = 1)]
   public class UserInterfaceData : ScriptableObject
   {
      public GameObject DefaultInteractUI;
   
      public Sprite interactionDialogueSprite;
      public Sprite interactionQuestionSprite;
      public Sprite interactionShopSprite;
      public Sprite interactionOtherSprite;
   }
}
