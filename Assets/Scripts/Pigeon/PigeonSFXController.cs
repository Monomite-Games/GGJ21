using Palomas.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSFXController : SFXController
{
    public List<AudioClip> pigeonClips;

    private void Start()
    {
        StartCoroutine(TriggerSound());
    }

    private IEnumerator TriggerSound()
    {
        float waitTime;

        while (true)
        {
            waitTime = Random.Range(1f, 7f);
            yield return new WaitForSeconds(waitTime);
            PlayRandomClip(pigeonClips);
        }
    }
}
