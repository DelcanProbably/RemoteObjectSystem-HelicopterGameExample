using UnityEngine;

/// <summary>
/// A RemoteAudioSource allows sounds to be played from connected Remotes.
/// </summary>
public class RemoteAudioSource : RemoteComponent {

    AudioSource fallbackAudioSource;

    protected override void RemoteComponentAwake() {
        moduleName = "audio";
    }

    public override void ActivateFallback() {
        fallbackMode = true;
        
        fallbackAudioSource = gameObject.AddComponent<AudioSource>();
    }
    
    // Play a sound from the remote device.
    public void Play (RemoteSound sound) {
        if (fallbackMode) {
            // If we're in fallback mode, just play the sound through the attached audio source.
            AudioClip clip = sound.clip;
            fallbackAudioSource.PlayOneShot(clip);
            return;
        }

        SendCommand("play", sound);
    }

    public void SetAudioConfig(RemoteAudioConfig config) {
        throw new System.NotImplementedException();
    }

}