using UnityEngine;
using System.Collections;


static partial class Utilities
{
    public static IEnumerator WaitForSoundFinish(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
    }

}
