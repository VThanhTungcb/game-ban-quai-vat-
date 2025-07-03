using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Lượng máu của enemy
    public int health;

    // Vị trí của người chơi (sẽ được gán khi bắt đầu)
    [HideInInspector]
    public Transform player;

    // Tốc độ di chuyển của enemy
    public float speed;

    // Thời gian giữa các lần tấn công
    public float timeBetweenAttacks;

    // Lượng sát thương gây ra cho người chơi mỗi lần tấn công
    public int damage;

    // Tỉ lệ rơi item (pickup), tính theo phần trăm (0 - 100)
    public int pickupChance;

    // Danh sách các vật phẩm có thể rơi
    public GameObject[] pickups;

    // Tỉ lệ rơi máu hồi phục, cũng tính theo phần trăm
    public int healthPickupChance;

    // Prefab vật phẩm hồi máu
    public GameObject healthPickup;

    // Hiệu ứng khi enemy chết
    public GameObject deathEffect;

    // Hàm Start chạy khi enemy được sinh ra
    public virtual void Start()
    {
        // Tìm người chơi theo tag "Player" và lưu lại vị trí để đuổi theo
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Gọi khi enemy bị nhận sát thương
    public void TakeDamage(int amount)
    {
        // Trừ máu
        health -= amount;

        // Nếu máu về 0 hoặc nhỏ hơn thì enemy chết
        if (health <= 0)
        {
            // Xác suất rơi vật phẩm thông thường
            int randomNumber = Random.Range(0, 101); // Từ 0 đến 100
            if (randomNumber < pickupChance)
            {
                // Chọn ngẫu nhiên một vật phẩm từ danh sách
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            // Xác suất rơi vật phẩm hồi máu
            int randHealth = Random.Range(0, 101);
            if (randHealth < healthPickupChance)
            {
                Instantiate(healthPickup, transform.position, transform.rotation);
            }

            // Sinh hiệu ứng chết và xoá enemy khỏi màn chơi
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
