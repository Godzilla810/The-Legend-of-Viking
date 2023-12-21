using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    //Player Movement
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform playerCamera;

    //Parameter
    private bool isGrounded = true;
    private Vector3 playerVelocity;
    private IAttackable enemy;


    void Update()
    {
        HandleMovement();
        HandleAttack();
        HandleRotation();
        HandleJump();
    }

    public override void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal + vertical) != 0)
        {
            Vector3 moveDirection = (playerCamera.forward * vertical + playerCamera.right * horizontal).normalized;
            if (Input.GetKey(KeyCode.LeftShift))
                Run(moveDirection);
            else
                Walk(moveDirection);
        }
        else
            Idle();
    }
    public override void HandleAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null && Input.GetMouseButtonDown(0))
                {
                    Attack(enemy);
                }
            }
        }
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //Player rotate
        transform.Rotate(Vector3.up * mouseX * rotationSpeed);

        // Rotate the camera up/down without moving the character
        float desiredRotationX = playerCamera.transform.eulerAngles.x - mouseY;
        playerCamera.rotation = Quaternion.Euler(desiredRotationX, playerCamera.eulerAngles.y, 0f);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            playerAnim.SetTrigger("Jump");
        }

        isGrounded = characterController.isGrounded;

        if (isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
