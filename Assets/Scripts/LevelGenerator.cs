using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    static LevelGenerator Singleton;
    [SerializeField] GameObject[] soldiers;
    [SerializeField] GameObject[] enemies;
    [SerializeField] float difficultyRatio;
    [SerializeField] float absoluteMaxDifficulty = 0.9f;
    [SerializeField] float density;
    [SerializeField] float absoluteMaxDensity = 0.9f;

    [SerializeField] float difficultyIncreaseMultiplier = 1.0f;
    [SerializeField] float densityIncreaseMultiplier = 1.0f;


    [SerializeField] Transform[] spawnPoints;

    public static List<GameObject> Level;

    private void Awake() {
        Level = new List<GameObject>();
        Singleton = this;
    }

    private void Start() {
        GenerateLevel();
    }

    public void GenerateLevel () {

        // Destroy existing level
        foreach (GameObject g in Level) {
            Destroy(g);
        }
        Level.Clear();

        // Always spawn at least one soldier, and keep track of where it has been spawned so we skip it for level generation
        int soldierSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(soldiers[Random.Range(0, soldiers.Length)], spawnPoints[soldierSpawnPoint]);

        for (int i = 0; i < spawnPoints.Length; i++) {
            if (i == soldierSpawnPoint) continue;
            Transform t = spawnPoints[i];
            if (Random.Range(0.0f, 1.0f) < Mathf.Min(density, absoluteMaxDensity)) {
                
                GameObject spawn;

                if (Random.Range(0.0f, 1.0f) < Mathf.Min(difficultyRatio, absoluteMaxDifficulty)) {
                    GameObject enemy = enemies[Random.Range(0, enemies.Length-1)];
                    spawn = Instantiate(enemy, t);
                } else {
                    GameObject soldier = soldiers[Random.Range(0, soldiers.Length-1)];
                    spawn = Instantiate(soldier, t);
                }
                
                spawn.transform.localPosition = Vector3.zero;

                Level.Add(spawn);
            }
        }
    }

    void DestroyLevel () {
        foreach(GameObject g in Level) {
            Destroy(g);
        }
    }

    public static void IncreaseDifficulty () {
        Singleton.difficultyRatio *= Singleton.difficultyIncreaseMultiplier;
        Singleton.density *= Singleton.densityIncreaseMultiplier;
    }
}
