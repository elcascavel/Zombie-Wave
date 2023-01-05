using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isRunning = false;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float speed = 5f;
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        if (isRunning)
        {
            controller.Move(transform.TransformDirection(moveDirection) * speed * 2 * Time.deltaTime);
        }
        else
        {
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
