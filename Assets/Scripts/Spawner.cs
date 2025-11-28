using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform player;
    public float spawnInterval = 1.5f;
    public float spawnRadius = 12f;
    public int maxAlive = 25;

    float timer;
    int alive;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && alive < maxAlive)
        {
            timer = 0f;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        if (!player) return;

        float ang = Random.value * Mathf.PI * 2f;
        Vector2 pos = (Vector2)player.position + new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)) * spawnRadius;

        var z = Instantiate(zombiePrefab, pos, Quaternion.identity);
        var hp = z.GetComponent<Health>();
        if (hp)
        {
            // when the zombie dies, decrement alive and destroy it
            hp.onDeath.AddListener(() =>
            {
                Destroy(z);
                alive--;
                if (ScoreManager.I) ScoreManager.I.Add(1); // +1 score
            });
        }
        alive++;
    }
}



