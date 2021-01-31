using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Palomas.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SFXController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource AudioSource;

        protected void PlayClip(AudioClip clip)
        {
            if(AudioSource != null)
            {
                AudioSource.PlayOneShot(clip);
            }
        }

        protected void PlayRandomClip(ICollection<AudioClip> clips)
        {
            AudioClip randomClip = GameUtils.RandomElement(clips);

            PlayClip(randomClip);
        }

        protected void Stop()
        {
            AudioSource.Stop();
        }
    }
}