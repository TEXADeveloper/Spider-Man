using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float horizontalSensibility;
    [SerializeField] private float verticalSensibility;
    [SerializeField] private float gamePadHorizontalSensibility;
    [SerializeField] private float gamePadVerticalSensibility;
    Vector2 lookDir = Vector2.zero;
    float xRotation = 0;
    bool pad = false;

    void Start()
    {
        PlayerInputScript.CamEvent += cameraInput;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void cameraInput(Vector2 input, bool pad)
    {
        lookDir = input;
        this.pad = pad;
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, lookDir.x * ((pad) ? gamePadHorizontalSensibility : horizontalSensibility) * Time.fixedDeltaTime, 0));

        xRotation -= lookDir.y * ((pad) ? gamePadVerticalSensibility : verticalSensibility) * Time.fixedDeltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    void OnDestroy()
    {
        PlayerInputScript.CamEvent -= cameraInput;
    }
}
