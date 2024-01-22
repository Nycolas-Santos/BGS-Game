using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Scripts
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSettings audioSettings;
        
        [SerializeField]private AudioSource _uiAudioSource;
        [SerializeField]private AudioSource _sfxAudioSource;

        private bool _footstepSoundPlaying;
        private const float FOOTSTEP_DELAY = 0.275f;

        public void PlayUISound(AudioClip audioClip)
        {
            _uiAudioSource.PlayOneShot(audioClip);
        }

        public void PlaySFXSound(AudioClip audioClip, bool randomPitch)
        {
            if (randomPitch) _sfxAudioSource.pitch = Random.Range(0.9f, 1.1f);
            else
            {
                _sfxAudioSource.pitch = 1;
            }
            _sfxAudioSource.PlayOneShot(audioClip);
        }

        public void PlayFootstepSound()
        {
            if (_footstepSoundPlaying) return;
            StartCoroutine(IEPlayFootstepSound());
        }

        private IEnumerator IEPlayFootstepSound()
        {
            _footstepSoundPlaying = true;
            PlaySFXSound(audioSettings.sfxFootstepSound,true);
            yield return new WaitForSeconds(FOOTSTEP_DELAY);
            _footstepSoundPlaying = false;
        }
    }
}
