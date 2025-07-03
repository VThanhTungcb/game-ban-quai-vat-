using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script giúp một đối tượng (ví dụ: con trỏ tùy chỉnh) đi theo chuột
public class CursorFollow : MonoBehaviour
{
    // Ẩn con trỏ chuột mặc định khi game bắt đầu
    private void Start()
    {
        Cursor.visible = false;
    }

    // Cập nhật vị trí của đối tượng theo vị trí chuột mỗi frame
    private void Update()
    {
        // Gán vị trí của đối tượng bằng vị trí của chuột (tính theo màn hình - Screen Space)
        transform.position = Input.mousePosition;
    }
}
