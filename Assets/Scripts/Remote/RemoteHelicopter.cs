using UnityEngine;

public class RemoteHelicopter : MonoBehaviour
{
    RemoteAudioSource rAudioSource;
    RemoteArduino rArduino;
    [SerializeField] int[] ledPins = new int[3];
    [SerializeField] RemoteAudioClip[] propellerSounds;
    [SerializeField] RemoteAudioClip deathSound;

    [SerializeField] float defaultVolume = 0.5f;

    private void Awake() {
        rAudioSource = GetComponent<RemoteAudioSource>();
        rArduino = GetComponent<RemoteArduino>();
    }

    private void Start() {
        rAudioSource.SetVolume(defaultVolume);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F11)) {
            SetPinModes();
        }
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
        // Invoke soldier update to avoid incorrect lit pins
        UpdateNumSoldiers(GetComponent<Helicopter>().heldSoldiers);
    }

    public void UpdateNumSoldiers(int soldiers) {
        for (int i = 0; i < ledPins.Length; i++) {
            rArduino.DigitalWrite(ledPins[i], i <= soldiers-1 ? 1 : 0);
        }
    }
}
