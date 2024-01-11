using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputScript : MonoBehaviour
{
    PlayerInput pI;

    public static event Action<Vector2> MoveEvent;
    public static event Action<Vector2, bool> CamEvent;
    public static event Action<bool> JumpEvent;
    public static event Action<bool> RunEvent;
    public static event Action<bool, bool> SwingEvent;

    void Start()
    {
        pI = this.GetComponent<PlayerInput>();
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        MoveEvent?.Invoke(ctx.ReadValue<Vector2>());
    }

    public void MouseCameraInput(InputAction.CallbackContext ctx)
    {
        CamEvent?.Invoke(ctx.ReadValue<Vector2>(), false);
    }

    public void PadCameraInput(InputAction.CallbackContext ctx)
    {
        CamEvent?.Invoke(ctx.ReadValue<Vector2>(), true);
    }

    public void JumpInput(InputAction.CallbackContext ctx)
    {
        JumpEvent?.Invoke(ctx.performed);
    }

    public void RunInput(InputAction.CallbackContext ctx)
    {
        RunEvent?.Invoke(ctx.performed);
    }

    public void FallInput(InputAction.CallbackContext ctx)
    {
        JumpEvent?.Invoke(ctx.performed);
    }

    public void RightSwingInput(InputAction.CallbackContext ctx)
    {
        SwingEvent?.Invoke(ctx.performed, false);
    }

    public void LeftSwingInput(InputAction.CallbackContext ctx)
    {
        SwingEvent?.Invoke(ctx.performed, true);
    }
}
