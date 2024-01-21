using System.Collections;
using System.Collections.Generic;
using Game.Core.Scripts;
using UnityEngine;

public class Shopkeeper : MonoBehaviour, IInteractable
{
    private GameObject interactPrompt;

    private const float HEIGHT_ABOVE_INTERACTABLE = 1f;
    private const string STARTING_INTERACTION_LOG = "Start Interaction";
    private const string FINISH_INTERACTION_LOG = "Finished Interaction";
    
    public void Interact()
    {
        UserInterfaceManager.Instance.OpenShop();
        UserInterfaceManager.Instance.OpenInventory();
    }

    public void StartInteraction()
    {
        Debug.Log(STARTING_INTERACTION_LOG);
        GameManager.Instance.PlayerInstance.SetState(PlayerState.Interacting);
        ShopController.OnCloseShop += FinishInteraction;
        DestroyInteractionPrompt();
    }

    public void FinishInteraction()
    {
        Debug.Log(FINISH_INTERACTION_LOG);
        GameManager.Instance.PlayerInstance.SetState(PlayerState.Idle);
        ShopController.OnCloseShop -= FinishInteraction;
        CreateInteractionPrompt();
    }

    public void CanInteract(bool value)
    {
        if (value)
        {
            CreateInteractionPrompt();
        }
        else
        {
            DestroyInteractionPrompt();
        }
    }

    private void CreateInteractionPrompt()
    {
        interactPrompt = Instantiate(UserInterfaceManager.Instance.UserInterfaceData.DefaultInteractUI);
        interactPrompt.transform.position = transform.position + new Vector3(0,HEIGHT_ABOVE_INTERACTABLE, 0);
        interactPrompt.GetComponent<InteractPrompt>().InteractionType = InteractionType.Shop;
    }

    private void DestroyInteractionPrompt()
    {
        if (interactPrompt != null) Destroy(interactPrompt);
    }
}
