using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;

    private PlayerLook look;

    private PlayerWeaponSelector playerWeaponSelector;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        playerWeaponSelector = GetComponent<PlayerWeaponSelector>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Shoot.performed += ctx => playerWeaponSelector.activeWeapon.Shoot();
        //onFoot.Reload.performed += ctx => weaponController.Reload();
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
