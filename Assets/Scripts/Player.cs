using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static int Score;
    public float speed;
    public float smoothTime = 0.05f; // Thời gian làm mượt

    // Rigidbody2D để xử lý vật lý
    private Rigidbody2D rb;
    private Vector2 moveAmount;
    private Vector2 moveVelocity = Vector2.zero; // dùng cho SmoothDamp

    private Animator anim;

    // Máu hiện tại của người chơi
    public int health;
    public GameObject[] hearts;

    // Hình trái tim đầy và trái tim rỗng
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Animator riêng cho hiệu ứng khi bị thương
    public Animator hurtAnim;
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

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveAmount = moveInput.normalized * speed;
        if (moveInput != Vector2.zero)

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
        else
        {
        else {
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
        // Làm mượt di chuyển bằng SmoothDamp
        Vector2 newPosition = Vector2.SmoothDamp(rb.position, rb.position + moveAmount * Time.fixedDeltaTime, ref moveVelocity, smoothTime);
        rb.MovePosition(newPosition);
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    // Hàm xử lý khi nhân vật bị trúng đòn
    public void TakeDamage(int amount)
        Instantiate(hurtSound, transform.position, Quaternion.identity);
       Instantiate(hurtSound, transform.position, Quaternion.identity);
        health -= amount;

        // Cập nhật giao diện trái tim
        UpdateHealthUI(health);

        // Animation bị thương

        hurtAnim.SetTrigger("hurt");
        if (health <= 0)
        {
            Destroy(this.gameObject); // Xoá người chơi
            sceneTransitions.LoadScene("Lose"); // Chuyển sang màn thua
        }
    }
    public void ChangeWeapon(Weapon weaponToEquip)
    {
    public void ChangeWeapon(Weapon weaponToEquip) {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));

        // Tạo vũ khí mới tại vị trí người chơi
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }
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
    public void Heal(int healAmount)
    {
    public void Heal(int healAmount) {
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
