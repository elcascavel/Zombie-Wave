using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isRunning = false;
    private bool isRegeneratingStamina = false;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float speed = 5f;
    public bool IsRunning { get => isRunning; set => isRunning = value; }

    public PlayerStats playerStats;
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

        if (isRunning && !isRegeneratingStamina)
        {
            playerStats.DrainStamina(1);
            if (playerStats.CurrentStamina > 0)
            {
                controller.Move(transform.TransformDirection(moveDirection) * speed * 2 * Time.deltaTime);
            }
            else
            {
                isRunning = false;
            }
        }
        else
        {
            controller.Move(playerVelocity * Time.deltaTime);
            if (playerStats.CurrentStamina < 100)
            {
                isRegeneratingStamina = true;
                playerStats.RegenerateStamina();
            }
            else if (playerStats.CurrentStamina >= 100)
            {
                isRegeneratingStamina = false;
            }
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
