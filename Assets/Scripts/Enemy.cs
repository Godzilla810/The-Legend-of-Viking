using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float detectRange;
    [SerializeField] private GameObject player;

    private float targetDistance;
    private float attactCountdown = 1.5f;

    public void Update()
    {
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
        HandleMovement();
        HandleAttack();
    }
    public override void HandleMovement()
    {
        if (targetDistance < detectRange)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
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

    void OnDrawGizmosSelected()     //�bScene���Ϥ�ø�s�ոթΥi���ƫH�����ϧΤ���
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detectRange);
    }
}
