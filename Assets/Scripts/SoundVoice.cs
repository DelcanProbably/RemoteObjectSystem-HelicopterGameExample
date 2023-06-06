using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVoice : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    public bool IsPlaying() {
        return audioSource.isPlaying;
    }

}
