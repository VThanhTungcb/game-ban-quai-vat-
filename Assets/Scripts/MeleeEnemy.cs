using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp MeleeEnemy kế thừa từ Enemy — là quái cận chiến tấn công người chơi khi ở gần
public class MeleeEnemy : Enemy
{

    public float stopDistance;       // Khoảng cách dừng lại và bắt đầu tấn công
    private float attackTime;        // Thời gian được phép tấn công tiếp theo
    public float attackSpeed;        // Tốc độ lao vào tấn công

    private void Update()
    {
        if (player != null)
        {
            // Nếu còn cách xa người chơi, tiếp tục di chuyển đến gần
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else // Nếu đã đủ gần, bắt đầu tấn công
            {
                if (Time.time >= attackTime)
                {
                    attackTime = Time.time + timeBetweenAttacks; // Đặt lại thời gian tấn công tiếp theo
                    StartCoroutine(Attack());                   // Bắt đầu coroutine tấn công
                }
            }
        }
    }

    // Coroutine thực hiện hiệu ứng lao tới người chơi và gây sát thương
    IEnumerator Attack()
    {
        player.GetComponent<Player>().TakeDamage(damage);  // Gây sát thương cho người chơi

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;

            // Tạo hiệu ứng lao vào bằng công thức parabol: -x^2 + x
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, interpolation);

            yield return null;
        }
    }
}
