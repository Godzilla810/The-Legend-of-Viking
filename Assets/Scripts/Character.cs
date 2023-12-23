using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : MonoBehaviour, IAttackable
{
    [SerializeField] protected float walkSpeed = 5f;
    [SerializeField] protected float runSpeed = 10f;
    [SerializeField] protected float rotationSpeed = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] protected float attackRange = 3f;
    [SerializeField] private float health = 100f;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image colorPart;
    [SerializeField] private GameObject coin;

    protected CharacterController characterController;
    protected Animator characterAnim;
    protected AudioSource characterAudio;

    protected float currentHealth;

    public void Start()
    {
        currentHealth = health;
        characterController = GetComponent<CharacterController>();
        characterAnim = GetComponent<Animator>();
        characterAudio = GetComponent<AudioSource>();
    }

    public virtual void HandleMovement() { }
    public virtual void HandleAttack() { }
    public void Idle()
    {
        characterAnim.SetBool("isWalk", false);
    }
    public void Walk(Vector3 moveDirection)
    {
        characterController.Move(moveDirection * walkSpeed * Time.deltaTime);
        characterAnim.SetBool("isRun", false);
        characterAnim.SetBool("isWalk", true);
    }
    public void Run(Vector3 moveDirection)
    {
        characterController.Move(moveDirection * runSpeed * Time.deltaTime);
        characterAnim.SetBool("isRun", true);
    }
    public void Attack(IAttackable target)
    {
        characterAnim.SetTrigger("Attack");
        target.TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        colorPart.fillAmount = currentHealth / health;
        characterAnim.SetTrigger("Hit");
        if (currentHealth == 0)
        {
            Die();
        }
    }
    public void Recover(float amount)
    {
        currentHealth += amount;
        colorPart.fillAmount = currentHealth / health;
    }
    public virtual void Die()
    {
        Instantiate(coin, transform.position + new Vector3(0, 1, 0), transform.rotation);
        characterAnim.SetTrigger("Die");
        healthBar.SetActive(false);
        enabled = false;
    }
}
public interface IAttackable
{
    void Attack(IAttackable target);
    void TakeDamage(float damage);
    void Die();
}