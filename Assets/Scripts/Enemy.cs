using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float detectRange;

    private GameObject player;
    private float targetDistance;
    private float attactCountdown = 1.5f;

    public void Update()
    {
        player = GameObject.Find("Player");
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
        HandleMovement();
        HandleAttack();
    }
    public override void HandleMovement()
    {
        if (targetDistance < detectRange)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            //在兩個旋轉之間平滑過渡
            Vector3 smoothRotation = Quaternion.Lerp(transform.rotation,
            rotation, Time.deltaTime * rotationSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, smoothRotation.y, 0f);
            Walk(lookDirection);
        }
        else
            Idle();
    }
    public override void HandleAttack()
    {
        if (targetDistance < attackRange)
        {
            attactCountdown -= Time.deltaTime;
            if (attactCountdown <= 0.001f)
            {
                Attack(player.GetComponent<Player>());
                attactCountdown = 1.5f;
            }
        }
    }

    void OnDrawGizmosSelected()     //在Scene視圖中繪製調試或可視化信息的圖形元素
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectRange);
    }
}
