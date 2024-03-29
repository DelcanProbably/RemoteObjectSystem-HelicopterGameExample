using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteIdentificationSkipButton : MonoBehaviour
{
    Button btn;
    [SerializeField] GameObject showWhenActive;
    [SerializeField] GameObject showWhenPaused;
    [SerializeField] float pauseTime;

    private void Awake() {
        btn = GetComponent<Button>();
    }

    private void Update() {
        // In addition to the button calling the skip function, manually call when the Escape key is pressed to skip.
        if (RemoteManager.DebugKeysEnabled && Input.GetKeyDown(KeyCode.Escape)) {
            Skip();
        }
    }

    public void Skip () {
        StartCoroutine(SkipFlow());
    }

    void SetActiveState(bool b) {
        showWhenActive.SetActive(b);
        showWhenPaused.SetActive(!b);
        btn.interactable = b;
    }

    IEnumerator SkipFlow() {
        SetActiveState(false);
        yield return new WaitForSecondsRealtime(pauseTime);
        SetActiveState(true);
    }
}
