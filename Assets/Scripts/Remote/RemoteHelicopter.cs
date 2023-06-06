using UnityEngine;

public class RemoteHelicopter : MonoBehaviour
{
    RemoteAudioSource rAudioSource;
    [SerializeField] RemoteSound[] propellerSounds;
    [SerializeField] RemoteSound deathSound;

    private void Awake() {
        rAudioSource = GetComponent<RemoteAudioSource>();
    }

    public void PlayPropellerSound(int i) {
        rAudioSource.Play(propellerSounds[i]);
    }

    public void OnDeath () {
        rAudioSource.Play(deathSound);
    }
}
