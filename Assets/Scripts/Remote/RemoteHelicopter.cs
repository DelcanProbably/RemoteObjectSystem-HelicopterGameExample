using UnityEngine;

public class RemoteHelicopter : MonoBehaviour
{
    RemoteAudioSource rAudioSource;
    RemoteArduino rArduino;
    [SerializeField] int[] ledPins = new int[3];
    [SerializeField] RemoteAudioClip[] propellerSounds;
    [SerializeField] RemoteAudioClip deathSound;

    private void Awake() {
        rAudioSource = GetComponent<RemoteAudioSource>();
        rArduino = GetComponent<RemoteArduino>();
    }

    public void PlayPropellerSound(int i) {
        rAudioSource.Play(propellerSounds[i]);
    }

    public void OnDeath () {
        rAudioSource.Play(deathSound);
    }

    public void SetPinModes() {
        foreach (int i in ledPins) {
            rArduino.SetPinMode(i, RemoteArduino.PinMode.Output);
        }
    }

    public void UpdateNumSoldiers(int soldiers) {
        for (int i = 0; i < ledPins.Length; i++) {
            rArduino.DigitalWrite(ledPins[i], i <= soldiers-1 ? 1 : 0);
        }
    }
}
