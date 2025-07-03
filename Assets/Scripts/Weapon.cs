using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Prefab của đạn (projectile) sẽ được bắn ra
    public GameObject projectile;

    // Vị trí nơi đạn được sinh ra
    public Transform shotPoint;

    // Khoảng thời gian giữa 2 lần bắn
    public float timeBetweenShots;

    // Thời gian xác định khi nào có thể bắn tiếp
    private float shotTime;

    // Animator dùng để tạo hiệu ứng rung camera khi bắn
    Animator cameraAnim;

    private void Start()
    {
        // Lấy Animator gắn trên camera chính
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    private void Update()
    {
        // Tính toán hướng từ vũ khí đến con trỏ chuột
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Tính góc xoay để vũ khí hướng theo con trỏ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Tạo góc quay (trừ 90 độ do sprite ban đầu hướng lên trên)
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        // Gán góc quay cho vũ khí
        transform.rotation = rotation;

        // Kiểm tra nếu người chơi nhấn chuột trái
        if (Input.GetMouseButton(0))
        {
            // Nếu đã đủ thời gian giữa các lần bắn
            if (Time.time >= shotTime)
            {
                // Tạo viên đạn mới tại shotPoint với góc quay hiện tại
                Instantiate(projectile, shotPoint.position, transform.rotation);

                // Gọi animation "shake" trên camera để tạo hiệu ứng rung khi bắn
                cameraAnim.SetTrigger("shake");

                // Đặt lại thời gian cho lần bắn tiếp theo
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}
