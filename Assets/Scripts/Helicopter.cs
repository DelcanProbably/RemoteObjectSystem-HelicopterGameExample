using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] int lives = 3;
    [SerializeField] float respawnTime = 1.0f;
    [SerializeField] SpriteRenderer spriteRenderer;
    Vector2 startPos;

    // Movement speed
    [SerializeField] float speed = 2.0f;
    [SerializeField] float rotateSpeed = 1.0f;
    float rotorAmount;
    // Number of soldiers that the helicopter can hold simultaneously
    [SerializeField] int maxSoldiers;

    // Slightly wacky system to keep UI up to date without heaps of copied code.
    public int heldSoldiers {get; private set;}

    [SerializeField] AudioClip pickupSoldierClip;
    [SerializeField] AudioClip[] chopperClips;
    [SerializeField] float chopDelay;
    [SerializeField] float chopVelocityAmount;
    [SerializeField] float minimumChopDelay;
    [SerializeField] AudioClip hospitalDepositClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] GameObject explosionParticleEffect;

    [SerializeField] float speedUpgradeMultiplier = 1.5f;

    bool alive = true;

    float speedTimer;
    [SerializeField] float speedBonusThreshold = 5.0f;

    // ROS
    RemoteHelicopter remoteHelicopter;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        // ROS
        remoteHelicopter = GetComponent<RemoteHelicopter>();
     
    }
    
    private void Start() {
        ClearHeldSoldiers();
        UIManager.UpdateLives(lives);
        StartCoroutine(SoundCoroutine());
        startPos = transform.position;
    }
    
    void Update() {
        if (speedTimer < speedBonusThreshold) {
            speedTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        if (!alive) return;
        PhysicsMovement();
    }

    void StandardMovement() {
        // Take axes input and apply to rigidbody velocity
        Vector2 input = Vector2.ClampMagnitude(new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1.0f);
        rb.velocity = input * speed;
    }

    void PhysicsMovement() {
        Vector2 input = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float equilibrium = -Physics.gravity.y * rb.mass;
        rb.AddForce((Vector2)transform.up * equilibrium + (Vector2)transform.up * input.y * speed);
        rb.SetRotation(input.x * -rotateSpeed);
        // rb.AddTorque(input.x * -rotateSpeed);
    }

    IEnumerator SoundCoroutine() {
        int chopperClip = 0;
        while (true) {
            
            Vector2 input = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            float delay = Mathf.Clamp(chopDelay - chopVelocityAmount * input.sqrMagnitude, minimumChopDelay, Mathf.Infinity);
            yield return new WaitForSeconds(delay);
            if (!alive) continue;

            // ROS
            // DON'T PLAY SOUND - this is now handled by ROS
            // SoundManager.PlayClip(chopperClips[chopperClip]);
            remoteHelicopter.PlayPropellerSound(chopperClip);
            chopperClip++;
            if (chopperClip == chopperClips.Length) chopperClip = 0;
        }
    }
    
    public void SoldierCollision (Soldier soldier) {
        // Should only pick up soldier if not holding max soldiers
        if (heldSoldiers < maxSoldiers) {
            GameManager.AddPoints(GameManager.capturePoints, soldier.transform.position);
            // Destroy the soldier world object
            Destroy(soldier.gameObject);
            // Add to soldiers on the helicopter
            AddHeldSoldier();
        }
    }

    void AddHeldSoldier() {
        if (speedTimer < speedBonusThreshold) {
            speedTimer = 0;
        }
        SetHeldSoldiers(heldSoldiers + 1);
        SoundManager.PlayClip(pickupSoldierClip);
    }

    void ClearHeldSoldiers() {
        SetHeldSoldiers(0);
    }

    void SetHeldSoldiers (int value) {
        heldSoldiers = value;
        UIManager.UpdateHelicopterSoldiers(heldSoldiers);
        remoteHelicopter.UpdateNumSoldiers(value);
    }

    public void DepositSoldiers () {
        if (heldSoldiers > 0) {
            // Tell the game manager we've rescued soldiers
            GameManager.SoldierRescued(heldSoldiers);
            GameManager.AddPoints(heldSoldiers * GameManager.rescuePoints, transform.position);
            SoundManager.PlayClip(hospitalDepositClip);
            if (speedTimer < speedBonusThreshold) {
                Store.AddCoins(heldSoldiers);
                SpeedyText.SpeedBonus();
            }
            speedTimer = 0;
            // Clear soldiers in helicopter
            ClearHeldSoldiers();
        }
    }

    public void UpgradeCapacity () {
        maxSoldiers++;
        UIHeldSoldiers.IncreaseSlots();
    }

    public void Die () {
        if (!alive) return;
    
        // Take one life
        lives--;
        UIManager.UpdateLives(lives);
        alive = false;

        Instantiate(explosionParticleEffect, transform.position, Quaternion.identity);

        // ROS
        // DO NOT play sound - managed by ROS
        // SoundManager.PlayClip(deathClip);
        remoteHelicopter.OnDeath();

        if (heldSoldiers > 0) {
            GameManager.AddPoints(heldSoldiers * GameManager.lostSoldierPoints, transform.position);
            ClearHeldSoldiers();
        }
    
        if (lives < 0) {
            // Call the end of the game as a loss.
            GameManager.EndGame(false);
            // If no lives left, destroy the helicopter. This could be replaced with somthing more visually interesting
            Destroy(gameObject);
        } else {
            // Otherwise respawn
            transform.position = startPos;
            rb.velocity = Vector2.zero;
            StartCoroutine(RespawnDelay());
        }
    }

    public void EarnLife () {
        lives++;
        UIManager.UpdateLives(lives);
    }

    public void UpgradeSpeed () {
        speed *= speedUpgradeMultiplier;
    }

    IEnumerator RespawnDelay () {
        alive = false;
        spriteRenderer.enabled = false;
        rb.isKinematic = true;
        transform.position = startPos;

        yield return new WaitForSeconds(respawnTime);
        
        alive = true;
        spriteRenderer.enabled = true;
        rb.isKinematic = false;
    }

}