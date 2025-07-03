using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script giúp camera theo dõi mục tiêu (thường là người chơi) trong một vùng giới hạn
public class CameraFollow : MonoBehaviour
{
    // Mục tiêu mà camera sẽ theo dõi (ví dụ: nhân vật chính)
    public Transform target;

    // Tốc độ di chuyển của camera để theo kịp mục tiêu
    public float speed;

    // Giới hạn di chuyển theo trục X và Y (để camera không vượt ra ngoài map)
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Gán vị trí camera bằng vị trí mục tiêu ngay khi bắt đầu
    private void Start()
    {
        transform.position = target.position;
    }

    // LateUpdate được gọi sau Update (giúp camera theo dõi mượt mà hơn sau khi đối tượng đã di chuyển)
    private void LateUpdate()
    {
        // Nếu có mục tiêu để theo dõi
        if (target != null)
        {
            // Giới hạn vị trí mục tiêu trong phạm vi được cho phép
            float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

            // Di chuyển camera mượt mà tới vị trí mục tiêu đã bị giới hạn
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);
        }
    }
}
