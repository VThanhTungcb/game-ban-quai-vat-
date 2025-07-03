using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Tốc độ di chuyển của người chơi
    public float speed;

    // Rigidbody2D để xử lý vật lý
    private Rigidbody2D rb;

    // Hướng di chuyển
    private Vector2 moveAmount;

    // Animator để điều khiển animation
    private Animator anim;

    // Máu hiện tại của người chơi
    public int health;

    // Mảng trái tim hiển thị trên UI
    public GameObject[] hearts;

    // Hình trái tim đầy và trái tim rỗng
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Animator riêng cho hiệu ứng khi bị thương
    public Animator hurtAnim;

    // Đối tượng quản lý chuyển cảnh
    private SceneTransition sceneTransitions;

    // Âm thanh khi bị thương
    public GameObject hurtSound;

    // Hiệu ứng để lại dấu vết khi chạy
    public GameObject trail;

    // Thời gian giữa các lần tạo dấu vết
    private float timeBtwTrail;
    public float startTimeBtwTrail;

    // Vị trí để sinh dấu vết (thường nằm dưới chân)
    public Transform groundPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransitions = FindObjectOfType<SceneTransition>();
    }

    private void Update()
    {
        // Nhận input từ bàn phím
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        // Nếu đang di chuyển
        if (moveInput != Vector2.zero)
        {
            // Tạo dấu vết khi chạy
            if (timeBtwTrail <= 0)
            {
                Instantiate(trail, groundPos.position, Quaternion.identity);
                timeBtwTrail = startTimeBtwTrail;
            }
            else
            {
                timeBtwTrail -= Time.deltaTime;
            }

            // Bật animation chạy
            anim.SetBool("isRunning", true);
        }
        else
        {
            // Tắt animation chạy
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        // Di chuyển nhân vật bằng Rigidbody (giúp mượt hơn)
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    // Hàm xử lý khi nhân vật bị trúng đòn
    public void TakeDamage(int amount)
    {
        // Phát âm thanh và hiệu ứng bị thương
        Instantiate(hurtSound, transform.position, Quaternion.identity);

        // Trừ máu
        health -= amount;

        // Cập nhật giao diện trái tim
        UpdateHealthUI(health);

        // Animation bị thương
        hurtAnim.SetTrigger("hurt");

        // Nếu máu <= 0 thì thua
        if (health <= 0)
        {
            Destroy(this.gameObject); // Xoá người chơi
            sceneTransitions.LoadScene("Lose"); // Chuyển sang màn thua
        }
    }

    // Hàm đổi vũ khí
    public void ChangeWeapon(Weapon weaponToEquip)
    {
        // Xoá vũ khí hiện tại (nếu có)
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));

        // Tạo vũ khí mới tại vị trí người chơi
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    // Cập nhật hình ảnh trái tim trên UI dựa theo máu hiện tại
    void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }

    // Hồi máu cho người chơi
    public void Heal(int healAmount)
    {
        if (health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }

        // Cập nhật trái tim
        UpdateHealthUI(health);
    }
}
