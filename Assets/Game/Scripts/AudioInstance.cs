using System.Collections;
using UnityEngine;

namespace Game.Scripts
{
    public class AudioInstance : MonoBehaviour
    {
        public IEnumerator Play(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(audioClip.length);
            Destroy(gameObject);
        }
    }
}
