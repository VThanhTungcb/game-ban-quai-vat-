using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script dùng để giữ nhạc nền chạy liên tục khi chuyển scene (singleton)
public class Music : MonoBehaviour
{
    // Biến static để giữ một thể hiện duy nhất của Music
    private static Music instance;

    // Awake được gọi trước Start, ngay khi object được tạo
    private void Awake()
    {
        // Nếu chưa có instance nào, gán instance này là duy nhất
        if (instance == null)
        {
            instance = this;

            // Không bị hủy khi load sang scene khác
            DontDestroyOnLoad(instance);
        }
        else
        {
            // Nếu đã có instance khác, hủy bản mới để tránh trùng lặp
            Destroy(gameObject);
        }
    }
}
