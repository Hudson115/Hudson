using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] bool groundedPlayer;
    [SerializeField] float playerSpeed;
    [SerializeField] float gravityValue;
    [SerializeField] GameObject activeChar;
    [SerializeField] float moveHorizontal;
    [SerializeField] float moveVertical;
    [SerializeField] float speed = 4;
    [SerializeField] float rotateSpeed = 4;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] bool isJumping;

    void Start()
    {
        playerSpeed = 4;
        gravityValue = -20;

    }


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            isJumping = true;
            activeChar.GetComponent<Animator>().Play("Jump");
            playerVelocity.y += 10;
            StartCoroutine(ResetJump());
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            this.gameObject.GetComponent<CharacterController>().minMoveDistance = 0.001f;
            if(isJumping == false)
            {
                activeChar.GetComponent<Animator>().Play("Standard Run");
            }
        }

        else
        {
            this.gameObject.GetComponent<CharacterController>().minMoveDistance = 0.901f;
            if (isJumping == false)
            {
                activeChar.GetComponent<Animator>().Play("Idle");
            }
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.9f);
        isJumping = false;
    }
}
