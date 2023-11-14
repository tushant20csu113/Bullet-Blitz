using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    PlayerInput playerInputMap;
    private void Awake()
    {
        playerInputMap = new PlayerInput();
    }
    private void OnEnable()
    {
        playerInputMap.Enable();
    }
    private void OnDisable()
    {
        playerInputMap.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerInputMap.Player.Attack.performed += ShootButtonPressed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveInput();
    }
    private void Update()
    {
        MousePosition();      
        
    }

    private void MousePosition()
    {
        Vector2 mouse_input = playerInputMap.Player.Aim.ReadValue<Vector2>();
        mouse_input = Camera.main.ScreenToWorldPoint(mouse_input);
        PlayerMovement.Instance.Aim(mouse_input);
    }

    private void MoveInput()
    {
        Vector2 input = playerInputMap.Player.Movement.ReadValue<Vector2>();
        PlayerMovement.Instance.Move(input.x, input.y);
    }

    private void ShootButtonPressed(InputAction.CallbackContext obj)
    {
        PlayerMovement.Instance.Shoot();
    }



}
