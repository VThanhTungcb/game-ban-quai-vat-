using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Tốc độ bay của viên đạn
    public float speed;

    // Thời gian tồn tại tối đa trước khi tự huỷ
    public float lifeTime;

    // Lượng sát thương gây ra khi trúng mục tiêu
    public int damage;

    // Hiệu ứng nổ khi bắn
    public GameObject explosion;

    // Âm thanh phát ra khi đạn được bắn
    public GameObject soundObject;

    // Dấu vết (vệt sáng) để lại khi đạn bay
    public GameObject trail;

    // Khoảng thời gian giữa mỗi lần tạo dấu vết
    private float timeBtwTrail;

    // Khoảng thời gian gốc giữa các lần để lại trail
    public float startTimeBtwTrail;

    private void Start()
    {
        // Gọi hàm tự huỷ đạn sau một thời gian (để tránh tồn tại mãi)
        Invoke("DestroyProjectile", lifeTime);

        // Tạo âm thanh bắn ra
        Instantiate(soundObject, transform.position, transform.rotation);

        // Tạo hiệu ứng khi đạn vừa sinh ra (hiệu ứng nổ đầu nòng)
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        // Tạo vệt sáng khi đạn di chuyển
        if (timeBtwTrail <= 0)
        {
            Instantiate(trail, transform.position, Quaternion.identity);
            timeBtwTrail = startTimeBtwTrail;
        }
        else
        {
            timeBtwTrail -= Time.deltaTime;
        }

        // Di chuyển đạn theo hướng lên (Vector2.up) với tốc độ đã đặt
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // Huỷ đạn và tạo hiệu ứng nổ tại vị trí hiện tại
    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Khi viên đạn va chạm với các đối tượng có tag nhất định
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Nếu chạm enemy thường → gây sát thương rồi huỷ đạn
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }

        // Nếu chạm boss → gây sát thương rồi huỷ đạn
        if (other.tag == "boss")
        {
            other.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
