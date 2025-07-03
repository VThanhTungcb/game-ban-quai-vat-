using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // Máu hiện tại của Boss
    public int health;

    // Danh sách các quái vật mà Boss có thể triệu hồi
    public Enemy[] enemies;

    // Khoảng cách khi sinh ra quái từ vị trí Boss
    public float spawnOffset;

    // Biến lưu lại nửa lượng máu ban đầu để kích hoạt giai đoạn 2
    private int halfHealth;

    // Animator để điều khiển animation của Boss
    private Animator anim;

    // Sát thương Boss gây ra khi chạm vào người chơi
    public int damage;

    // Hiệu ứng máu và hiệu ứng nổ khi Boss chết
    public GameObject blood;
    public GameObject effect;

    // Thanh máu của Boss trên UI
    private Slider healthBar;

    // Dùng để chuyển cảnh khi Boss bị tiêu diệt
    private SceneTransition sceneTransitions;

    private void Start()
    {
        // Tính toán máu một nửa
        halfHealth = health / 2;

        // Lấy Animator trên chính Boss
        anim = GetComponent<Animator>();

        // Tìm Slider đầu tiên trong scene (dùng làm thanh máu)
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;

        // Tìm đối tượng SceneTransition trong scene
        sceneTransitions = FindObjectOfType<SceneTransition>();
    }

    // Hàm gọi khi Boss nhận sát thương
    public void TakeDamage(int amount)
    {
        // Giảm máu và cập nhật thanh máu
        health -= amount;
        healthBar.value = health;

        // Nếu máu <= 0 thì Boss chết
        if (health <= 0)
        {
            // Hiệu ứng nổ và máu
            Instantiate(effect, transform.position, Quaternion.identity);
            Instantiate(blood, transform.position, Quaternion.identity);

            // Hủy đối tượng Boss
            Destroy(this.gameObject);

            // Ẩn thanh máu
            healthBar.gameObject.SetActive(false);

            // Chuyển sang cảnh chiến thắng
            sceneTransitions.LoadScene("Win");
        }

        // Nếu máu còn một nửa thì chuyển sang stage 2 (animation khác)
        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        // Boss triệu hồi ngẫu nhiên một enemy mới tại vị trí gần nó
        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    // Nếu va chạm với người chơi thì gây sát thương
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
