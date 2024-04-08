using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControls : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    public event Action<Vector3> PositionClicked;

    public event Action<Vector3> MouseHoverPositionChanged;

    private Camera _mainCam;

   [SerializeField] private PauseMenuController _pauseMenu;
    private Vector3 ConvertMousePosition(Vector2 positionInput)
    {

       Vector3 worldPos = _mainCam.ScreenToWorldPoint(positionInput);

        return worldPos;
    }

    private void Start()
    {
        _mainCam = Camera.main;
  
    }
    private void PauseMouse(bool pause)
    {
        if (pause) _playerInput.actions["Click"].performed -= ClickPosition;

        else _playerInput.actions["Click"].performed += ClickPosition;


    }
    private IEnumerator SubscribeDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _playerInput.actions["Click"].performed += ClickPosition;
    }
    private void OnEnable()
    {
        _playerInput.actions["Click"].performed += ClickPosition;

        _playerInput.actions["Pause"].performed += PauseGameInput;

       PauseMenuController.Pause += PauseMouse;
      
    }
    private void PauseGameInput(InputAction.CallbackContext context)
    {
        _pauseMenu.PauseWithMenuEvent();
    }
    private void OnDisable()
    {
        _playerInput.actions["Click"].performed -= ClickPosition;

        _playerInput.actions["Pause"].performed -= PauseGameInput;

        PauseMenuController.Pause -= PauseMouse;
    }
    private void FixedUpdate()
    {
        Debug.Log(ConvertMousePosition(_playerInput.actions["SetPosition"].ReadValue<Vector2>()));
       MouseHoverPositionChanged?.Invoke(ConvertMousePosition(_playerInput.actions["SetPosition"].ReadValue<Vector2>()));
       
    }

    private void ClickPosition(InputAction.CallbackContext context)
    {
   
         PositionClicked?.Invoke(ConvertMousePosition(_playerInput.actions["SetPosition"].ReadValue<Vector2>()));

    }
}
