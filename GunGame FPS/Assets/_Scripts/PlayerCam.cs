using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // Variables
    [Header("Game Settings")]
    [SerializeField] private float Xsens;
    [SerializeField] private float Ysens;

    private float xRotation;
    private float yRotation;

    [Header("References")]
    [SerializeField] private Transform orientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        // Get Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * Xsens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * Ysens;

        // No Idea how this works
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // -- Back At It Again
}
