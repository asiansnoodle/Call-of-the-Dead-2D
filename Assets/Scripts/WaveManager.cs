using UnityEngine;
using UnityEngine.UI;   // If you use TextMeshPro, see note below.
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;          // Player transform (for spawn around player)
    public GameObject zombiePrefab;   // Zombie prefab to spawn
    public TextMeshProUGUI waveText;  // UI text to display wave (or TMP type)

    [Header("Spawning")]
    public float spawnRadius = 10f;   // How far around the player zombies appear
    public float timeBetweenWaves = 3f;
    public int startingZombies = 5;   // Zombies in wave 1
    public int zombiesPerWaveIncrease = 3;

    [Header("Difficulty Scaling")]
    public float zombieHealthIncreasePerWave = 5f;
    public float zombieSpeedIncreasePerWave = 0.2f;

    private int currentWave = 0;
    private int zombiesToSpawnThisWave = 0;
    private bool isSpawning = false;
    private float waveCountdown = 0f;
    public int CurrentWave => currentWave;


    private void Start()
    {
        // Start first wave after a short delay
        waveCountdown = 1f;
    }

    private void Update()
    {
        // If we are currently spawning, don't do anything else in Update
        if (isSpawning) return;

        // If there are still enemies alive, wait
        int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesAlive > 0) return;

        // No enemies alive: count down to next wave
        waveCountdown -= Time.deltaTime;
        if (waveCountdown <= 0f)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        zombiesToSpawnThisWave = startingZombies + (currentWave - 1) * zombiesPerWaveIncrease;

        // Update UI
        if (waveText != null)
        {
            waveText.text = $"Wave: {currentWave}";
        }

        if (AudioManager.I != null)
        {
            AudioManager.I.PlayWaveStart();
        }


        // Start spawning
        StartCoroutine(SpawnWaveCoroutine());
    }

    private System.Collections.IEnumerator SpawnWaveCoroutine()
    {
        isSpawning = true;

        for (int i = 0; i < zombiesToSpawnThisWave; i++)
        {
            SpawnZombieForCurrentWave();
            // small delay between spawns so they don't all pop in at once
            yield return new WaitForSeconds(0.2f);
        }

        // Done spawning; start countdown to next wave *after* current wave is cleared
        isSpawning = false;
        waveCountdown = timeBetweenWaves;
    }

    private void SpawnZombieForCurrentWave()
    {
        if (player == null || zombiePrefab == null)
        {
            Debug.LogWarning("WaveManager: Missing player or zombiePrefab reference");
            return;
        }

        // Random point around the player in a circle
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = player.position + new Vector3(randomDirection.x, randomDirection.y, 0f) * spawnRadius;

        GameObject z = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

        var hp = z.GetComponent<Health>();
        if (hp)
        {
            // Health handles Destroy(gameObject) internally.
            // We ONLY care about score here.
            hp.onDeath.AddListener(() =>
            {
                if (ScoreManager.I) ScoreManager.I.Add(1);
                if (AudioManager.I != null)
                {
                    AudioManager.I.PlayZombieDeath();
                }
            });

            // OPTIONAL: health scaling
            int bonusHealth = (int)(zombieHealthIncreasePerWave * (currentWave - 1));
            if (bonusHealth > 0)
            {
                hp.SetMax(hp.max + bonusHealth);
            }
        }

        var movement = z.GetComponent<ZombieAI>();
        if (movement != null)
        {
            movement.speed += zombieSpeedIncreasePerWave * (currentWave - 1);
        }
    }

    // Optional: public method you can call from GameManager on Restart
    public void ResetWaves()
    {
        currentWave = 0;
        waveCountdown = 1f;
        isSpawning = false;

        if (waveText != null)
        {
            waveText.text = "Wave: 0";
        }
    }
}
