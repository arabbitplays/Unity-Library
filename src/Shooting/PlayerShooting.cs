using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShooting : MonoBehaviour
{

    //-------------References-------------

    [SerializeField] private InputManager inputManager;

    private Weapon weapon;

    [SerializeField]
    private WeaponStats stats;

    private float timeBetweenShooting;

    //---------------Shared Variables-----------
    
    public FloatReference bulletsLeft, magazineSize, ammoCount;


    //Initialize references and weapon
    private void Awake()
    {
        if (weapon == null) {
            weapon = new ExampleWeapon(stats, this, inputManager); 
        }
    }

    void Update()
    {
        if (weapon == null) {
            return;
        }

        //shoot input check
        if (inputManager.GetShootingInput(weapon.IsAllowButtonHold(), weapon.GetLoadTime()))
        {
            if (CanShoot()) {
                CalculateTimeBetweenShooting();

                //start new shoot cycle
                Shoot();
            }
        } 
        
        if (inputManager.shootingInputUp || bulletsLeft.value <= 0) { //shooting canceled
            weapon.OnStopShooting();
        }

        //reload check
        if (ammoCount.value > 0 && inputManager.reloadInput && bulletsLeft.value < magazineSize.value && weapon.TryReload()) {
            Invoke("Reload", weapon.reloadTime);
        } 
    }

    // shooting conditions
    private bool CanShoot() {
        if (weapon.TryToShoot() && bulletsLeft.value > 0) {
            return true;
        }

        return false;
    }

    private void CalculateTimeBetweenShooting() {
        timeBetweenShooting = 1 / weapon.shootingRate;
    }

    //when Weapon.Shoot() returns true -> calls itself
    //else reset shot with time delay
    private void Shoot() {
        if(weapon.Shoot()) {
            Invoke("Shoot", weapon.timeBetweenShots);
        } else {
            Invoke("ResetShot", timeBetweenShooting);
        }
    }

    //no startLoading effects when player cant shoot
    public void StartLoading() {
        if (bulletsLeft.value > 0) {
            weapon.OnStartLoading();
        }
    }

    public void EndLoading() {
        weapon.OnEndLoading();
    }

    //help methods to use Invoke for not monobehavior class weapon
    private void ResetShot() {
        weapon.ResetShot();
    }

    private void Reload() {
        weapon.Reload();
    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
    }

    public Weapon GetWeapon() {
        return weapon;
    } 
}
