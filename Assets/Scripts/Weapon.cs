using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject bulletPrefab; // this will be what the object actually looks like from my understanding 
    public Transform bulletPos; 
    public float bulletVelocity = 30f; 
    public float bulletLife = 3f; // time before bullet expires
    public float bulletCooldown = 0.1f; 
    private bool canFire = true; 

    public Transform playerOrientation; 
    public Transform weaponOrientation; 

    private IEnumerator DestroyBullet(GameObject bullet, float bulletLife) {
        yield return new WaitForSeconds(bulletLife); 
        Destroy(bullet); 
    }

    private IEnumerator GunCooldown(float bulletCooldown) {
        yield return new WaitForSeconds(bulletCooldown); 
        canFire = true; 
    }

    private void FireWeapon() { // instantiates the bullet by creatinfg a new bullet object
        canFire = false; 
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity); 
        bullet.GetComponent<Rigidbody>().AddForce(bulletPos.forward.normalized * bulletVelocity, ForceMode.Impulse); 
        StartCoroutine(DestroyBullet(bullet, bulletLife));  
        StartCoroutine(GunCooldown(bulletCooldown)); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire) {
            FireWeapon();  
        }
        weaponOrientation.rotation = playerOrientation.rotation; 
    }
}