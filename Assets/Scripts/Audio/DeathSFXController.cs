using UnityEngine;

namespace Palomas.Audio
{
    public class DeathSFXController : SFXController
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private AudioClip DeathSound;

        private void Start()
        {
            GameEvents.LifeLost += (sender, args) => { PlayClip(DeathSound); };
        }
    }
}
