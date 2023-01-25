using System;
using Game.Scripts.StateMachines.DayCycle;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Game.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        // FIELDS
        public DayCycleSettings dayCycleSettings;
        #region Properties
        public Player Player { get; set; }
        public Map CurrentMap { get; set; }
        public DayCycleStateMachine DayCycleStateMachine { get; set; }

        [Serializable]
        public struct DayCycleSettings
        {
            public float morningDuration; // in seconds
            public float afternoonDuration; // in seconds
            public float nightDuration; // in seconds
            public PostProcessVolume morningPostProcessing;
            public PostProcessVolume afternoonPostProcessing;
            public PostProcessVolume nightPostProcessing;
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            DayCycleStateMachine = gameObject.AddComponent<DayCycleStateMachine>();
            DayCycleStateMachine.Owner = this;
        }

        public void SwitchMap(Map newMap, Map oldMap)
        {
            CurrentMap = newMap;
            CurrentMap.gameObject.SetActive(true);
            oldMap.gameObject.SetActive(false);
        }

        public void Play2DAudio(AudioClip audioClip)
        {
            var audioGameObject = new GameObject("2DAudio-Instance");
            var audioSource = audioGameObject.AddComponent<AudioSource>();
            var audioInstance = audioGameObject.AddComponent<AudioInstance>();
            audioInstance.StartCoroutine(audioInstance.Play(audioSource,audioClip));
        }
        public void Play3DAudio(AudioClip audioClip, Vector3 position)
        {
            var audioGameObject = new GameObject("3DAudio-Instance");
            var audioSource = audioGameObject.AddComponent<AudioSource>();
            var audioInstance = audioGameObject.AddComponent<AudioInstance>();
            audioInstance.StartCoroutine(audioInstance.Play(audioSource,audioClip));
            audioSource.spatialize = true;
            audioSource.spatialBlend = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioGameObject.transform.position = position;
        }
    }
}
