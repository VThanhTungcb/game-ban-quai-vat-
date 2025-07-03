using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script điều khiển đạn được bắn ra từ Enemy
public class EnemyBullet : MonoBehaviour
{

    Player playerScript;               // Tham chiếu đến script của người chơi để gọi hàm gây sát thương
    Vector2 targetPosition;           // Vị trí mục tiêu (vị trí của người chơi tại thời điểm bắn)

    public float speed;               // Tốc độ bay của đạn
    public int damage;                // Sát thương gây ra khi trúng người chơi

    public GameObject effect;         // Hiệu ứng khi đạn chạm mục tiêu hoặc tới vị trí

    private void Start()
    {
        // Tìm đối tượng người chơi và lấy script "Player"
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Lưu lại vị trí của người chơi tại thời điểm đạn được tạo
        targetPosition = playerScript.transform.position;
    }

    private void Update()
    {
        // Nếu đạn đã đến đúng vị trí mục tiêu (vị trí người chơi lúc bắn)
        if ((Vector2)transform.position == targetPosition)
        {
            Instantiate(effect, transform.position, Quaternion.identity); // Tạo hiệu ứng nổ
            Destroy(gameObject); // Hủy viên đạn
        }
        else
        {
            // Di chuyển đạn đến vị trí mục tiêu với tốc độ đã định
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    // Xử lý va chạm
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Nếu đạn chạm vào người chơi
        if (other.tag == "Player")
        {
            playerScript.TakeDamage(damage);  // Gây sát thương
            Destroy(gameObject);             // Hủy viên đạn
        }
    }
}
