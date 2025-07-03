using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Lớp con đại diện cho 1 "wave" (làn quái)
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;              // Danh sách các loại enemy có thể sinh ra trong wave
        public int count;                    // Số lượng enemy trong wave này
        public float timeBetweenSpawns;      // Thời gian delay giữa mỗi lần spawn enemy
    }

    public Wave[] waves;                     // Mảng chứa tất cả các wave
    public Transform[] spawnPoints;          // Các điểm có thể spawn enemy
    public float timeBetweenWaves;           // Thời gian chờ giữa các wave

    private Wave currentWave;                // Wave hiện tại
    private int currentWaveIndex;            // Chỉ số wave hiện tại
    private Transform player;                // Vị trí người chơi

    private bool spawningFinished;           // Đánh dấu đã spawn xong wave

    public GameObject boss;                  // Prefab Boss sẽ xuất hiện cuối cùng
    public Transform bossSpawnPoint;         // Vị trí spawn Boss

    public GameObject healthBar;             // Thanh máu Boss sẽ bật khi Boss xuất hiện

    private void Start()
    {
        // Tìm người chơi theo tag "Player"
        player = GameObject.FindWithTag("Player").transform;

        // Bắt đầu gọi wave đầu tiên
        StartCoroutine(CallNextWave(currentWaveIndex));
    }

    private void Update()
    {
        // Khi đã spawn xong và không còn enemy nào trong scene
        if (spawningFinished == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            spawningFinished = false;

            if (currentWaveIndex + 1 < waves.Length)
            {
                // Tăng index để gọi wave tiếp theo
                currentWaveIndex++;
                StartCoroutine(CallNextWave(currentWaveIndex));
            }
            else
            {
                // Nếu đã hết tất cả các wave, gọi Boss xuất hiện
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                healthBar.SetActive(true); // Bật thanh máu của Boss
            }
        }
    }

    // Gọi wave sau một khoảng thời gian delay
    IEnumerator CallNextWave(int waveIndex)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(waveIndex));
    }

    // Thực hiện việc spawn enemy trong 1 wave
    IEnumerator SpawnWave(int waveIndex)
    {
        currentWave = waves[waveIndex];

        for (int i = 0; i < currentWave.count; i++)
        {
            // Nếu người chơi đã chết thì dừng luôn coroutine
            if (player == null)
            {
                yield break;
            }

            // Lấy ngẫu nhiên 1 enemy từ danh sách và 1 vị trí spawn
            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn enemy
            Instantiate(randomEnemy, randomSpawnPoint.position, transform.rotation);

            // Nếu là enemy cuối cùng thì đánh dấu đã spawn xong
            if (i == currentWave.count - 1)
            {
                spawningFinished = true;
            }
            else
            {
                spawningFinished = false;
            }

            // Chờ trước khi spawn enemy tiếp theo
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }
}
