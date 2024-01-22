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
    private const string STARTING_INTERACTION_LOG = "Start Interaction";
    private const string FINISH_INTERACTION_LOG = "Finished Interaction";
    
    public void Interact()
    {
        UserInterfaceManager.Instance.SubtitlesController.OnStartSubtitleLine += StartInteraction;
        UserInterfaceManager.Instance.SubtitlesController.OnFinishSubtitleLine += FinishInteraction;
        UserInterfaceManager.Instance.SubtitlesController.DisplaySubtitle(inspectableText,inspectableTextTime);
    }

    public void StartInteraction()
    {
        Debug.Log(STARTING_INTERACTION_LOG);
        GameManager.Instance.PlayerInstance.SetState(PlayerState.Interacting);
        SoundManager.Instance.PlayUISound(SoundManager.Instance.audioSettings.uiInspectSound);
        DestroyInteractionPrompt();
    }

    public void FinishInteraction()
    {
        Debug.Log(FINISH_INTERACTION_LOG);
        GameManager.Instance.PlayerInstance.SetState(PlayerState.Idle);
        UserInterfaceManager.Instance.SubtitlesController.OnStartSubtitleLine -= StartInteraction;
        UserInterfaceManager.Instance.SubtitlesController.OnFinishSubtitleLine -= FinishInteraction;
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
        if (interactPrompt != null) Destroy(interactPrompt);
        interactPrompt = Instantiate(UserInterfaceManager.Instance.UserInterfaceData.DefaultInteractUI);
        interactPrompt.transform.position = transform.position + new Vector3(0,HEIGHT_ABOVE_INTERACTABLE, 0);
        interactPrompt.GetComponent<InteractPrompt>().InteractionType = InteractionType.Question;
    }

    private void DestroyInteractionPrompt()
    {
        if (interactPrompt != null) Destroy(interactPrompt);
    }
}
