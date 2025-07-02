using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;

    public GameObject explosion;
    public GameObject soundObject;
    public GameObject trail;

    private float timeBtwTrail;
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
        Instantiate(soundObject, transform.position, transform.rotation);
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
