using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    // Thời gian tồn tại tối đa trước khi tự huỷ
    public float lifeTime;

    // Lượng sát thương gây ra khi trúng mục tiêu
    public int damage;

    // Hiệu ứng nổ khi bắn
    public GameObject explosion;
    public GameObject soundObject;
    public GameObject trail;

    private float timeBtwTrail;

    // Khoảng thời gian gốc giữa các lần để lại trail
    public float startTimeBtwTrail;

    private Rigidbody2D rb;

    private void Start()
    {
        // Lấy Rigidbody2D và áp lực bắn
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // ✅ Bật trọng lực
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse); // ✅ Bắn theo hướng đang nhìn

        // Hiệu ứng âm thanh + nổ
        Invoke("DestroyProjectile", lifeTime);

        // Tạo âm thanh bắn ra
        Instantiate(soundObject, transform.position, transform.rotation);

        // Tạo hiệu ứng khi đạn vừa sinh ra (hiệu ứng nổ đầu nòng)
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private Vector3 trailSmoothVelocity = Vector3.zero;

    private void Update()
    {
        // Hiệu ứng trail làm mượt theo vị trí cũ (mềm mại hơn)
        if (timeBtwTrail <= 0)
        {
            Vector3 smoothTrailPosition = Vector3.SmoothDamp(transform.position, transform.position, ref trailSmoothVelocity, 0.02f);
            Instantiate(trail, smoothTrailPosition, Quaternion.identity);
            timeBtwTrail = startTimeBtwTrail;
        }
        else
        {
            timeBtwTrail -= Time.deltaTime;
        }

        // ❌ Không dùng transform.Translate — đã dùng Rigidbody2D để xử lý lực và trọng lực
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Khi viên đạn va chạm với các đối tượng có tag nhất định
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }

        if (other.CompareTag("boss"))
        {
            other.GetComponent<Boss>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
