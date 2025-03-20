using UnityEngine;

public class AimDownSight : MonoBehaviour
{
    public float zoomSpeed = 20f; // adjust for desired zoom speed
    public float zoomInDistance = 20f; // adjust for desired zoom-in distance
    public float zoomOutDistance = 60f; // adjust for desired zoom-out distance
    private float _currentZoomDistance;

    void Start()
    {
        _currentZoomDistance = Camera.main.fieldOfView; // get initial FOV
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            _currentZoomDistance = zoomInDistance;
        } else if (Input.GetKeyUp(KeyCode.Mouse1)) {
            _currentZoomDistance = zoomOutDistance; 
        }

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _currentZoomDistance, Time.deltaTime * zoomSpeed);
    }
}
