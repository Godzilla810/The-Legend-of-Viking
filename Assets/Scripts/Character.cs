using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : MonoBehaviour, IAttackable
{
    [SerializeField] protected float walkSpeed = 5f;
    [SerializeField] protected float runSpeed = 10f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] protected float attackRange = 3f;
    [SerializeField] private float health = 100f;
    [SerializeField] private Image colorPart;

    protected CharacterController characterController;
    protected Animator playerAnim;
    protected AudioSource playerAudio;

    private float currentHealth;

    public void Start()
    {
        currentHealth = health;
        characterController = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    public virtual void HandleMovement() { }
    public virtual void HandleAttack() { }
    public void Idle()
    {
        playerAnim.SetBool("isWalk", false);
    }
    public void Walk(Vector3 moveDirection)
    {
        characterController.Move(moveDirection * walkSpeed * Time.deltaTime);
        playerAnim.SetBool("isRun", false);
        playerAnim.SetBool("isWalk", true);
    }
    public void Run(Vector3 moveDirection)
    {
        characterController.Move(moveDirection * runSpeed * Time.deltaTime);
        playerAnim.SetBool("isRun", true);
    }
    public void Attack(IAttackable target)
    {
        playerAnim.SetTrigger("Attack");
        target.TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetHelthUI();
    }
    public void SetHelthUI()
    {
        colorPart.fillAmount = currentHealth / health;
    }
}
public interface IAttackable
{
    void Attack(IAttackable target);
    void TakeDamage(float damage);
}