using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Dùng để chuyển scene

// Script xử lý chuyển cảnh có hiệu ứng (animation transition)
public class SceneTransition : MonoBehaviour
{
    // Animator để điều khiển hiệu ứng chuyển cảnh
    private Animator transitionAnim;

    // Gọi khi game bắt đầu (hoặc khi object này được khởi tạo)
    private void Start()
    {
        // Lấy component Animator gắn trên object này
        transitionAnim = GetComponent<Animator>();
    }

    // Hàm công khai cho phép gọi từ nơi khác để chuyển scene
    public void LoadScene(string sceneName)
    {
        // Gọi Coroutine để bắt đầu hiệu ứng và chuyển cảnh
        StartCoroutine(Transition(sceneName));
    }

    // Coroutine xử lý quá trình chuyển cảnh với hiệu ứng
    IEnumerator Transition(string sceneName)
    {
        // Kích hoạt trigger "end" để chạy animation chuyển cảnh (phải tạo trigger "end" trong Animator)
        transitionAnim.SetTrigger("end");

        // Chờ 1 giây để animation chạy xong trước khi chuyển cảnh
        yield return new WaitForSeconds(1);

        // Tải scene mới theo tên
        SceneManager.LoadScene(sceneName);
    }
}
