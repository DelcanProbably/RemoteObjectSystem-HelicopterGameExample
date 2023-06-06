using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager Singleton;

    [SerializeField] SoundVoice voicePrefab;
    int voices = 8;

    List<SoundVoice> soundVoices = new List<SoundVoice>();
    int nextVoice = 0;

    private void Awake() {
        Singleton = this;
    }

    private void Start() {
        for (int i = 0; i < voices; i++) {
            soundVoices.Add(Instantiate(voicePrefab, transform));
        }
    }

    public static void PlayClip(AudioClip clip) {
        Singleton.soundVoices[Singleton.nextVoice].PlayClip(clip);
    }



}
