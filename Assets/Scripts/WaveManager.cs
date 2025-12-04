using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;         
    public GameObject zombiePrefab;   
    public TextMeshProUGUI waveText;  

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

    // new stuff for difficulty
    private float spawnMultiplier = 1f;
    private float healthMultiplier = 1f;
    private float speedMultiplier = 1f;

    private void Start()
    {
        SetupDifficultyMultipliers();   // added for difficulty
        waveCountdown = 1f;
    }

    private void Update()
    {
        if (isSpawning) return;

        int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesAlive > 0) return;

        waveCountdown -= Time.deltaTime;
        if (waveCountdown <= 0f)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        int baseCount = startingZombies + (currentWave - 1) * zombiesPerWaveIncrease;   // edited for difficulty scaling
        zombiesToSpawnThisWave = Mathf.RoundToInt(baseCount * spawnMultiplier);

        if (waveText != null)
        {
            waveText.text = $"Wave: {currentWave}";
        }

        if (WaveAnnouncement.I != null)
        {
            WaveAnnouncement.I.ShowWave(currentWave);
        }

        if (AudioManager.I != null)
        {
            AudioManager.I.PlayWaveStart();
        }

        StartCoroutine(SpawnWaveCoroutine());
    }

    private System.Collections.IEnumerator SpawnWaveCoroutine()
    {
        isSpawning = true;

        for (int i = 0; i < zombiesToSpawnThisWave; i++)
        {
            SpawnZombieForCurrentWave();
            yield return new WaitForSeconds(0.2f);
        }

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

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = player.position + new Vector3(randomDirection.x, randomDirection.y, 0f) * spawnRadius;

        GameObject z = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
        // diff edits
        var hp = z.GetComponent<Health>();
        if (hp)
        {
            hp.onDeath.AddListener(() =>
            {
                if (ScoreManager.I) ScoreManager.I.Add(1);
                if (AudioManager.I != null)
                {
                    AudioManager.I.PlayZombieDeath();
                }
            });

            int bonusHealth = (int)(zombieHealthIncreasePerWave * (currentWave - 1) * healthMultiplier);
            if (bonusHealth > 0)
            {
                hp.SetMax(hp.max + bonusHealth);
            }
        }

        var movement = z.GetComponent<ZombieAI>();
        if (movement != null)
        {
            movement.speed += zombieSpeedIncreasePerWave * (currentWave - 1) * speedMultiplier;
        }
    }

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

    private void SetupDifficultyMultipliers()
    {
        switch (GameSettings.CurrentDifficulty)
        {
            case Difficulty.Easy:
                spawnMultiplier = 0.75f;   
                healthMultiplier = 0.1f;   
                speedMultiplier  = 0.3f;   
                break;

            case Difficulty.Normal:
                spawnMultiplier = 1f;
                healthMultiplier = 1f;
                speedMultiplier  = 1f;
                break;

            case Difficulty.Hard:
                spawnMultiplier = 3.0f;    
                healthMultiplier = 3.0f; 
                speedMultiplier  = 3.0f;  
                break;
        }

        Debug.Log($"WaveManager: difficulty={GameSettings.CurrentDifficulty}, " +
                $"spawn x{spawnMultiplier}, hp x{healthMultiplier}, speed x{speedMultiplier}");
    }
}
