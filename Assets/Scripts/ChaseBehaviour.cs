using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script kế thừa từ StateMachineBehaviour, dùng để điều khiển hành vi đuổi theo người chơi trong Animator (hệ thống State Machine của Unity)
public class ChaseBehaviour : StateMachineBehaviour
{

    private GameObject player;   // Đối tượng người chơi
    public float speed;          // Tốc độ di chuyển của enemy khi đuổi

    // Gọi khi state (trạng thái) mới được kích hoạt (vào trạng thái chase)
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tìm đối tượng người chơi theo tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Gọi mỗi frame khi còn trong trạng thái chase
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Nếu tìm thấy người chơi
        if (player != null)
        {
            // Di chuyển đối tượng chứa Animator (enemy) về phía người chơi
            animator.transform.position = Vector2.MoveTowards(
                animator.transform.position,
                player.transform.position,
                speed * Time.deltaTime
            );
        }
    }

    // Gọi khi thoát khỏi trạng thái này (khi rời trạng thái chase)
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Không cần xử lý gì khi thoát trạng thái
    }
}
