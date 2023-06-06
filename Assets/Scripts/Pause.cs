using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool paused = false;

    [SerializeField] GameObject pauseMenu;

    private void Awake() {
        paused = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            pauseMenu.SetActive(paused);
            Time.timeScale = paused ? 0 : 1;
        }
    }

    public void ReturnToGame () {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Quit () {
        Application.Quit();
    }
}
