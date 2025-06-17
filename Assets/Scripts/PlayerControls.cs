using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] bool groundedPlayer;
    [SerializeField] float playerSpeed;
    [SerializeField] float gravityValue;
    [SerializeField] GameObject activeChar;
    [SerializeField] float speed = 4;
    [SerializeField] float sprintMultiplier = 2; // Sprint multiplier
    [SerializeField] float rotateSpeed = 4;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] bool isJumping;
    [SerializeField] float sensitivity = 2;
    private float verticalRotation = 0f;

    void Start()
    {
        playerSpeed = 4;
        gravityValue = -20;
        Cursor.lockState = CursorLockMode.Locked; // Lock and hide the cursor
        Cursor.visible = false; // Hide the cursor
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        // Reset vertical velocity if grounded
        if (groundedPlayer)
        {
            if (playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                isJumping = false; // Reset jumping state when grounded
            }
        }

        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

        // Clamp vertical rotation
        verticalRotation -= mouseY * sensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f); // Limit the vertical rotation

        // Apply rotation for looking around
        transform.Rotate(0, mouseX * sensitivity, 0); // Y-axis rotation
        Camera.main.transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeed = speed * Input.GetAxis("Vertical");
        float sideSpeed = speed * Input.GetAxis("Horizontal");

        // Sprint functionality
        if (Input.GetKey(KeyCode.LeftControl))
        {
            curSpeed *= sprintMultiplier; // Increase speed when sprinting
            sideSpeed *= sprintMultiplier; // Increase side speed when sprinting
        }

        // Allow jumping even when stationary
        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            activeChar.GetComponent<Animator>().Play("Jump");
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue); // Calculate jump velocity
            isJumping = true; // Set jumping state immediately
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move((forward * curSpeed + right * sideSpeed + playerVelocity) * Time.deltaTime); // Updated to include sideways movement

        // Animation handling
        if (curSpeed != 0 || sideSpeed != 0)
        {
            this.gameObject.GetComponent<CharacterController>().minMoveDistance = 0.001f;
            if (!isJumping)
            {
                activeChar.GetComponent<Animator>().Play("Standard Run");
            }
        }
        else if (groundedPlayer) // Check if grounded even when stationary
        {
            this.gameObject.GetComponent<CharacterController>().minMoveDistance = 0.901f;
            if (!isJumping)
            {
                activeChar.GetComponent<Animator>().Play("Idle");
            }
        }
        
        if (transform.position.y < -10)
        {
            ScoreControl.totalScore = 0;
            SceneManager.LoadScene("Scene1");
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.9f);
        isJumping = false;
    }
}
