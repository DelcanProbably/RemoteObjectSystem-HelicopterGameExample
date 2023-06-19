using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RemoteAudioSource))]
public class RemoteShop : MonoBehaviour
{
    RemoteAudioSource remoteAudioSource;
    [SerializeField] RemoteAudioClip receiveMoney;
    [SerializeField] RemoteAudioClip chaching;

    private void Awake() {
        remoteAudioSource = GetComponent<RemoteAudioSource>();
    }

    public void OnScore () {
        remoteAudioSource.Play(receiveMoney);
    }

    public void OnPurchase () {
        remoteAudioSource.Play(chaching);
    }
}
