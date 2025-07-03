using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script điều khiển vật phẩm hồi máu cho người chơi khi chạm vào
public class HealthPickup : MonoBehaviour
{
    // Tham chiếu tới script của người chơi để gọi hàm Heal
    Player playerScript;

    // Lượng máu sẽ hồi khi nhặt vật phẩm
    public int healAmount;

    // Hiệu ứng (particle, animation...) khi người chơi nhặt vật phẩm
    public GameObject effect;

    // Hàm Start được gọi khi vật phẩm được tạo ra
    private void Start()
    {
        // Tìm đối tượng có tag "Player" và lấy script Player từ đó
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Hàm được gọi khi có đối tượng khác đi vào trigger collider của vật phẩm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu đối tượng chạm vào có tag là "Player"
        if (collision.tag == "Player")
        {
            // Tạo hiệu ứng tại vị trí của vật phẩm
            Instantiate(effect, transform.position, Quaternion.identity);

            // Gọi hàm Heal trong script Player để hồi máu
            playerScript.Heal(healAmount);

            // Hủy vật phẩm sau khi được nhặt
            Destroy(gameObject);
        }
    }
}
