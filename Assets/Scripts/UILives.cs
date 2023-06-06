using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILives : MonoBehaviour
{
    [SerializeField] Transform livesHolder;
    [SerializeField] GameObject lifePrefab;

    List<GameObject> lifeIcons = new List<GameObject>();

    public void UpdateLives (int lives) {
        int delta = lives - lifeIcons.Count;
        if (delta > 0) {
            while (delta > 0) {
                lifeIcons.Add(Instantiate(lifePrefab, livesHolder));
                delta--;
            }
        } else if (delta < 0) {
            if (lifeIcons.Count == 0) return;
            while (delta < 0) {
                GameObject lifeToDestroy = lifeIcons[0];
                lifeIcons.Remove(lifeToDestroy);
                Destroy(lifeToDestroy);
                delta++;
            }
        }
    }
}
