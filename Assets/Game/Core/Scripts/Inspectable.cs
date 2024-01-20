using System.Collections;
using System.Collections.Generic;
using Game.Core.Scripts;
using UnityEngine;

public class Inspectable : MonoBehaviour, IInteractable
{
    private GameObject interactPrompt;
    [SerializeField] private string inspectableText;
    [SerializeField] private float inspectableTextTime;

    private const float HEIGHT_ABOVE_INTERACTABLE = 1f;
    
    public void Interact()
    {
        UserInterfaceManager.Instance.SubtitlesController.OnStartSubtitleLine += StartInteraction;
        UserInterfaceManager.Instance.SubtitlesController.OnFinishSubtitleLine += FinishInteraction;
        UserInterfaceManager.Instance.SubtitlesController.DisplaySubtitle(inspectableText,inspectableTextTime);
    }

    public void StartInteraction()
    {
        Debug.Log("Start Interaction");
        GameManager.Instance.PlayerInstance.SetState(PlayerState.Interacting);
    }

    public void FinishInteraction()
    {
        Debug.Log("Finished Interaction");
        GameManager.Instance.PlayerInstance.SetState(PlayerState.Idle);
        UserInterfaceManager.Instance.SubtitlesController.OnStartSubtitleLine -= StartInteraction;
        UserInterfaceManager.Instance.SubtitlesController.OnFinishSubtitleLine -= FinishInteraction;
    }

    public void CanInteract(bool value)
    {
        if (value)
        {
            interactPrompt = Instantiate(UserInterfaceManager.Instance.UserInterfaceData.DefaultInteractUI);
            interactPrompt.transform.position = transform.position + new Vector3(0,HEIGHT_ABOVE_INTERACTABLE, 0);
            interactPrompt.GetComponent<InteractPrompt>().InteractionType = InteractionType.Question;
        }
        else
        {
            if (interactPrompt != null) Destroy(interactPrompt);
        }
    }
}
