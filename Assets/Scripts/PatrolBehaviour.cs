using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp này điều khiển hành vi tuần tra cho enemy bằng Animator State Machine
public class PatrolBehaviour : StateMachineBehaviour
{

    public float speed;  // Tốc độ di chuyển trong quá trình tuần tra

    private GameObject[] patrolPoints;  // Các điểm tuần tra (đặt tag là "point" trong scene)
    int randomPoint;  // Vị trí điểm tuần tra ngẫu nhiên hiện tại

    // Hàm này gọi khi enemy bắt đầu vào trạng thái "Tuần tra"
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tìm tất cả các điểm tuần tra trong scene (đã gắn tag "point")
        patrolPoints = GameObject.FindGameObjectsWithTag("point");

        // Chọn ngẫu nhiên một điểm để di chuyển tới
        randomPoint = Random.Range(0, patrolPoints.Length);
    }

    // Hàm này gọi mỗi frame khi enemy đang ở trạng thái "Tuần tra"
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Di chuyển enemy đến điểm tuần tra được chọn
        animator.transform.position = Vector2.MoveTowards(
            animator.transform.position,
            patrolPoints[randomPoint].transform.position,
            speed * Time.deltaTime
        );

        // Nếu enemy gần tới điểm đã chọn, chọn điểm mới để tiếp tục di chuyển
        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < 0.1f)
        {
            randomPoint = Random.Range(0, patrolPoints.Length);
        }
    }

    // Hàm này gọi khi enemy rời khỏi trạng thái "Tuần tra"
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Không cần xử lý gì thêm khi thoát khỏi trạng thái tuần tra
    }
}
