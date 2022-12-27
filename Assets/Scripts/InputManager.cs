using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;

    private PlayerLook look;

    private PlayerWeaponSelector playerWeaponSelector;

    bool pressed = false;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        playerWeaponSelector = GetComponent<PlayerWeaponSelector>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Shoot.performed += ctx => pressed = true;
        onFoot.Shoot.canceled += ctx => pressed = false;
        //onFoot.Reload.performed += ctx => weaponController.Reload();
    }

    void Update()
    {
        OnShoot();
    }

    void OnShoot()
    {
        if (pressed)
        {
            playerWeaponSelector.activeWeapon.Shoot();
        }
    }

    void Start()
    {
        onEnable();
    }

    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void onEnable()
    {
        onFoot.Enable();
    }

    private void onDisable()
    {
        onFoot.Disable();
    }
}
