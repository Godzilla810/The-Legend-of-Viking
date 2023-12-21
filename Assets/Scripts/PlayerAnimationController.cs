using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isRunning;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimationParameters();
    }

    void UpdateAnimationParameters()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if the player is moving
        isRunning = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        // Set animator parameters
        animator.SetBool("IsRunning", isRunning);
    }

    public void PlayJumpAnimation()
    {
        // Trigger the jump animation
        animator.SetTrigger("Jump");
    }

    public void PlayAttackAnimation()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");
    }
}
