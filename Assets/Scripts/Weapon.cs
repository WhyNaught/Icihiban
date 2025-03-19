using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject bulletPrefab; // this will be what the object actually looks like from my understanding 
    public Transform bulletPos; 
    public float bulletVelocity = 30f; 
    public float bulletLife = 3f; // time before bullet expires
    public float bulletCooldown = 0.1f; 
    private bool canFire = true; 

    private int magazineSize = 12; // size of ammo we start with 
    private int ammo = 12; // keeps track of how much ammmo we have 
    private float reloadTime = 2f; 

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

    private IEnumerator Reload(float reloadTime) {
        canFire = false; // we make sure that we cannot fire during the reload 
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        ammo = magazineSize; 
        canFire = true; 
        Debug.Log("Reloaded weapon!");
    }

    private void FireWeapon() { // instantiates the bullet by creatinfg a new bullet object
        canFire = false; 
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity); 
        bullet.GetComponent<Rigidbody>().AddForce(bulletPos.forward.normalized * bulletVelocity, ForceMode.Impulse); 
        StartCoroutine(DestroyBullet(bullet, bulletLife));  
        StartCoroutine(GunCooldown(bulletCooldown));
        ammo--; 
        if (ammo <= 0) {StartCoroutine(Reload(reloadTime));} // auto call reload is we need to 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && ammo > 0) {
            FireWeapon();  
        } else if (Input.GetKey(KeyCode.R) && ammo < magazineSize) { // this should stop us from reloading unnecessarily
            StartCoroutine(Reload(reloadTime));
        }
        weaponOrientation.rotation = playerOrientation.rotation; 
    }
}