using UnityEngine;

public class Bullet : MonoBehaviour {

    private  void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Person")) {
            Debug.Log("Hit another person!");
            Destroy(gameObject); 
        } else if (collision.gameObject.CompareTag("Terrain")) {
            Debug.Log("Hit the terrain!");
            Destroy(gameObject); 
        } 
    }
}