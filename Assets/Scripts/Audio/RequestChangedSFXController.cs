using UnityEngine;

namespace Palomas.Audio
{
    public class RequestChangedSFXController : SFXController
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private AudioClip HeySound;

        private void Start()
        {
            GameEvents.RequestChanged += (sender, args) => { PlayClip(HeySound); };
        }
    }
}
