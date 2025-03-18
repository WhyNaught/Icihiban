using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    
    // variables for sensitivity 
    public float sensX; public float sensY; 
    public Transform orientation; 
    float xRotation; float yRotation; 

    void Start() {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
    }

    void Update() {
        // mouse input 
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX; xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        // rotation application with quaternion
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); 
    }
}
