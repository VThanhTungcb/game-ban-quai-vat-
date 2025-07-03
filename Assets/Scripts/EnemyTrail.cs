using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script tạo hiệu ứng "vết mờ" phía sau enemy (dùng cho hiệu ứng bóng, vệt chạy, v.v.)
public class EnemyTrail : MonoBehaviour
{
    // Prefab của vết mờ (trail) sẽ được tạo ra
    public GameObject trail;

    // Biến đếm thời gian giữa các lần tạo vết mờ
    private float timeBtwSpawn;

    // Khoảng thời gian cố định giữa mỗi lần spawn vết mờ
    public float startTimeBtwSpawn;

    // Hàm Update được gọi mỗi frame
    private void Update()
    {
        // Nếu đã đến lúc tạo vết mờ mới
        if (timeBtwSpawn <= 0)
        {
            // Tạo một bản sao của prefab trail tại vị trí hiện tại, không xoay
            Instantiate(trail, transform.position, Quaternion.identity);

            // Reset lại thời gian chờ để tạo vết tiếp theo
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            // Giảm thời gian chờ theo thời gian trôi qua giữa các frame
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
