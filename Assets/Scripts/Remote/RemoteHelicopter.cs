using UnityEngine;

public class RemoteHelicopter : MonoBehaviour
{
    RemoteAudioSource rAudioSource;
    [SerializeField] RemoteSound[] propellerSounds;
    [SerializeField] RemoteSound deathSound;

    public void PlayPropellerSound(int i) {
        rAudioSource.Play(propellerSounds[i]);
    }

    public void OnDeath () {
        rAudioSource.Play(deathSound);
    }
}
