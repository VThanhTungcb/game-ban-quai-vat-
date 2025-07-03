using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script điều khiển vật phẩm cho người chơi nhặt vũ khí mới
public class Pickup : MonoBehaviour
{
    // Vũ khí sẽ được trang bị khi người chơi nhặt vật phẩm
    public Weapon weaponToEquip;

    // Hiệu ứng xuất hiện khi người chơi nhặt vật phẩm
    public GameObject effect;

    // Hàm được gọi khi một Collider khác đi vào vùng trigger của vật phẩm
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu đối tượng chạm vào có tag là "Player"
        if (collision.tag == "Player")
        {
            // Tạo hiệu ứng nhặt vật phẩm tại vị trí hiện tại
            Instantiate(effect, transform.position, Quaternion.identity);

            // Gọi hàm ChangeWeapon trong script Player để thay vũ khí hiện tại bằng vũ khí mới
            collision.GetComponent<Player>().ChangeWeapon(weaponToEquip);

            // Hủy đối tượng vật phẩm sau khi nhặt
            Destroy(gameObject);
        }
    }
}
