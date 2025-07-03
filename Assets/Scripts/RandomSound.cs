using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script phát một âm thanh ngẫu nhiên từ danh sách khi bắt đầu
public class RandomSound : MonoBehaviour
{
    // Component AudioSource dùng để phát âm thanh
    private AudioSource source;

    // Mảng chứa các âm thanh có thể phát
    public AudioClip[] clips;

    // Hàm Start được gọi khi đối tượng được khởi tạo
    private void Start()
    {
        // Lấy AudioSource gắn trên đối tượng này
        source = GetComponent<AudioSource>();

        // Tạo một số ngẫu nhiên trong khoảng từ 0 đến độ dài mảng clips
        int randomNumber = Random.Range(0, clips.Length);

        // Gán clip được chọn ngẫu nhiên cho AudioSource
        source.clip = clips[randomNumber];

        // Phát âm thanh
        source.Play();
    }
}
