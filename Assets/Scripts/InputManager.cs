using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerWeaponSelector playerWeaponSelector;
    private bool isReloading;
    bool pressed = false;
    bool reloadPressed = false;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        playerWeaponSelector = GetComponent<PlayerWeaponSelector>();
        onFoot.Jump.performed += ctx => motor.Jump();

        onFoot.Run.performed += ctx => motor.IsRunning = true;
        onFoot.Run.canceled += ctx => motor.IsRunning = false;

        onFoot.Shoot.performed += ctx => pressed = true;
        onFoot.Shoot.canceled += ctx => pressed = false;

        onFoot.Reload.performed += ctx => reloadPressed = true;
        onFoot.Reload.canceled += ctx => reloadPressed = false;
    }

    void Update()
    {
        playerWeaponSelector.activeWeapon.Tick(
            !isReloading
            && Application.isFocused && pressed
            && playerWeaponSelector.activeWeapon != null
        );

        if (ShouldManualReload())
        {
            isReloading = true;
            Debug.Log("Reloading");
        }
        else
        {
            OnShoot();
        }
    }

    private bool ShouldManualReload()
    {
        return !isReloading && reloadPressed && playerWeaponSelector.activeWeapon.CanReload();
    }

    private void EndReload()
    {
        playerWeaponSelector.activeWeapon.EndReload();
        isReloading = false;
        Debug.Log("Reloaded");
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
