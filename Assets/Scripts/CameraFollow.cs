using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraPos; 

    void Update() {
        transform.position = cameraPos.position; 
    }
}
