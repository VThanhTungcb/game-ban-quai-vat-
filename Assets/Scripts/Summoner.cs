using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp Summoner kế thừa từ Enemy — nghĩa là nó là một loại quái có thể triệu hồi thêm quái khác
public class Summoner : Enemy
{

    // Phạm vi di chuyển ngẫu nhiên (tọa độ giới hạn)
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    Vector2 targetPosition;  // Vị trí mục tiêu để Summoner di chuyển đến khi xuất hiện
    Animator anim;

    public float stopDistance;     // Khoảng cách dừng lại để tấn công người chơi
    private float attackTime;      // Thời điểm tấn công tiếp theo
    public float attackSpeed;      // Tốc độ di chuyển khi tấn công

    public Enemy enemyToSummon;    // Loại enemy mà Summoner sẽ triệu hồi
    public float timeBetweenSummons; // Thời gian giữa các lần triệu hồi
    private float summonTime;        // Thời điểm được phép triệu hồi tiếp theo

    // Ghi đè hàm Start từ lớp cha (Enemy)
    public override void Start()
    {
        base.Start();  // Gọi hàm Start() của lớp cha

        // Chọn vị trí mục tiêu ngẫu nhiên trong phạm vi giới hạn
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {

            // Nếu chưa đến vị trí mục tiêu thì di chuyển tới
            if ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);

                // Nếu đã đến nơi, kiểm tra thời gian để triệu hồi
                if (Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon"); // Gọi animation summon (sẽ gọi hàm Summon() sau đó)
                }
            }

            // Nếu người chơi ở trong phạm vi cho phép thì tấn công
            if (Vector2.Distance(transform.position, player.position) <= stopDistance)
            {
                if (Time.time >= attackTime)
                {
                    attackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());  // Gọi hàm tấn công với hiệu ứng di chuyển
                }
            }
        }
    }

    // Hàm được animation gọi khi chơi hiệu ứng triệu hồi
    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    // Coroutine tấn công: nhảy về phía người chơi rồi quay lại vị trí ban đầu
    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage); // Gây sát thương

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4; // Tạo hiệu ứng cong khi lao tới
            transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation);
            yield return null;
        }
    }
}
