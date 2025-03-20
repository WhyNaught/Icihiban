using System.Collections;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour {
    public GameObject bulletPrefab; // this will be what the object actually looks like from my understanding 
    public Transform bulletPos; 
    public float bulletVelocity = 30f; 
    public float bulletLife = 3f; // time before bullet expires
    public float bulletCooldown = 0.1f; 
    private bool canFire = true; 
    private bool reloading = false; 

    [Header("Ammunition")]
    public int magazineSize = 12; // size of ammo we start with 
    public int ammo = 12; // keeps track of how much ammmo we have 

    public TMP_Text ammoText; 

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
        reloading = true; 
        UpdateAmmoUi(); 
        yield return new WaitForSeconds(reloadTime);
        ammo = magazineSize; 
        canFire = true; 
        reloading = false; 
        UpdateAmmoUi(); 
    }

    private void FireWeapon() { // instantiates the bullet by creatinfg a new bullet object
        canFire = false; 
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity); 
        bullet.GetComponent<Rigidbody>().AddForce(bulletPos.forward.normalized * bulletVelocity, ForceMode.Impulse); 
        StartCoroutine(DestroyBullet(bullet, bulletLife));  
        StartCoroutine(GunCooldown(bulletCooldown));
        ammo--; 
        UpdateAmmoUi(); 
        if (ammo <= 0) {StartCoroutine(Reload(reloadTime));} // auto call reload is we need to 
    }

    private void UpdateAmmoUi() {
        if (ammoText != null && reloading == false) {
            ammoText.text = ammo.ToString() + " / " + magazineSize.ToString(); 
        } else if (ammoText != null) {
            ammoText.text = "Reloading..."; 
        }
    }

    void Start()
    {
        ammo = magazineSize; 
        UpdateAmmoUi(); 
    }

    void Update()
    {
        Transform oldParent = transform.parent;
        transform.parent = null;
        transform.localScale = new Vector3(0.2f, 0.2f, 1f); // set this to the scaling of the weapon in-game
        transform.parent = oldParent; 
        // if this ever breaks, we need to create a third object and store the player and the weapon under that object. 
        // we will need to add some code to make sure that the position of the weapon relative to the player stays the same though

        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && ammo > 0) {
            FireWeapon();  
        } else if (Input.GetKey(KeyCode.R) && ammo < magazineSize) { // this should stop us from reloading unnecessarily
            StartCoroutine(Reload(reloadTime));
        }
        weaponOrientation.rotation = playerOrientation.rotation; 
    }
}