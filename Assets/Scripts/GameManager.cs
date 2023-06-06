using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager Singleton;
    static int rescuedSoldiers;
    static int totalSoldiers;

    static int points;

    public const int capturePoints = 10;
    public const int rescuePoints = 100;
    public const int lostSoldierPoints = -50;



    [SerializeField] GameObject pointsPrefab;
    [SerializeField] LevelGenerator levelGenerator;

    private void Awake() {
        Singleton = this;
        // Reset totalSoldiers here so initial soldiers can be correctly counted.
        totalSoldiers = 0;
    }
    
    private void Start() {
        Initialise();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R))  Reset();
        if (Input.GetKeyDown(KeyCode.F10)) PlayerPrefs.SetInt("HighScore", 0);
    }

    void Initialise () {
        rescuedSoldiers = 0;
        points = 0;
        UIManager.UpdateSoldiers(rescuedSoldiers);
        UIManager.UpdatePoints(points);
    }

    void Reset () {
        // Just reload the scene. TODO: Loading screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void AddSoldier () {
        // Add to the soldier count
        totalSoldiers++;
    }

    // Called when soldiers are brought to the hospital. Points is the number of soldiers being deposited from the helicopter.
    public static void SoldierRescued (int soldiers) {
        rescuedSoldiers += soldiers;
        // If all the soldiers have been rescued then generate a new level
        if (FindObjectsOfType<Soldier>().Length == 0) {
            LevelGenerator.IncreaseDifficulty();   
            Singleton.levelGenerator.GenerateLevel();
        }
        // Earn currency
        Store.AddCoins(soldiers);
        // Update UI.
        UIManager.UpdateSoldiers(rescuedSoldiers);
    }

    public static void AddPoints (int amount, Vector3 position) {
        points += amount;
        GameObject text = Instantiate(Singleton.pointsPrefab, position, Quaternion.identity);
        text.GetComponentInChildren<PointsText>().SetText(amount);
        UIManager.UpdatePoints(points);

        // Kinda hacky copy-paste to check if we've lost points because of a helicopter crash, in which case the level might need to be regenerated
        if (amount < 0) {
            if (FindObjectsOfType<Soldier>().Length == 0) {
                LevelGenerator.IncreaseDifficulty();   
                Singleton.levelGenerator.GenerateLevel();
            }
        }
    }

    // Ends the game with a win or loss.
    public static void EndGame(bool win) {
        UIManager.EndGame(points);
        if (PlayerPrefs.GetInt("HighScore", 0) < points) {
            PlayerPrefs.SetInt("HighScore", points);
            UIManager.HighScore();
        }
    }
}
