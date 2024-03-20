using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    public event Action<Vector3> PositionClicked;

    public event Action<Vector3> MouseHoverPositionChanged;

    private Camera _mainCam;

   
    private Vector3 ConvertMousePosition(Vector2 positionInput)
    {

       Vector3 worldPos = _mainCam.ScreenToWorldPoint(positionInput);

        return worldPos;
    }

    private void Start()
    {
        _mainCam = Camera.main;

       
    }

    private void OnEnable()
    {
        _playerInput.actions["Click"].performed += ClickPosition;
    }

    private void OnDisable()
    {
        _playerInput.actions["Click"].performed -= ClickPosition;
    }
    private void Update()
    {
     
       MouseHoverPositionChanged?.Invoke( _mainCam.ScreenToWorldPoint(_playerInput.actions["SetPosition"].ReadValue<Vector2>()));
       
    }

    private void ClickPosition(InputAction.CallbackContext context)
    {
   
         PositionClicked?.Invoke(ConvertMousePosition(_playerInput.actions["SetPosition"].ReadValue<Vector2>()));

    }
}
