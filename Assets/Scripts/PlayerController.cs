using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 2f;
    public float jumpForce = 10f;
    public Transform playerCamera;

    private Rigidbody playerRb;
    private CharacterController characterController;
    private Animator playerAnim;
    private AudioSource playerAudio;

    private bool isGrounded = true;
    private Vector3 playerVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();  
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal + vertical) != 0)
        {
            Vector3 moveDirection = (playerCamera.forward * vertical + playerCamera.right * horizontal).normalized;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                characterController.Move(moveDirection * runSpeed * Time.deltaTime);
                playerAnim.SetBool("isRun", true);
            }
            else
            {
                characterController.Move(moveDirection * walkSpeed * Time.deltaTime);
                playerAnim.SetBool("isRun", false);
                playerAnim.SetBool("isWalk", true);
            }
        }
        else
        {
            playerAnim.SetBool("isWalk", false);
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
        playerCamera.transform.rotation = Quaternion.Euler(desiredRotationX, playerCamera.transform.eulerAngles.y, 0f);
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
    void OnCollisionEnter(Collision collision)
    {
        //¸¨¦a
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    isOnGround = true;
        //}
    }
}
