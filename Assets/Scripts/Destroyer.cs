using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script này tự động hủy đối tượng sau một khoảng thời gian
public class Destroyer : MonoBehaviour
{
    // Thời gian tồn tại của đối tượng (tính bằng giây)
    public float lifeTime;

    // Hàm Start được gọi khi đối tượng được khởi tạo
    private void Start()
    {
        // Hủy đối tượng này sau khoảng thời gian lifeTime
        Destroy(gameObject, lifeTime);
    }
}
