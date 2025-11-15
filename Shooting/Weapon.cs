using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public string name;

    //-------------References-------------

    protected PlayerShooting playerShooting;
    protected InputManager inputManager;
    protected Vector2 position;

    //----------Bullets-----------------

    protected BulletType[] bulletTypes;
    protected float bulletSize;


    //-------------Gun Stats-------------

    private WeaponStats stats;
    
    protected int bulletsPerTap;
    protected float  bulletSpeed;

    //timings public because playerShooting needs them (ScriptableObject?)
    public float shootingRate { get; protected set; }
    public float timeBetweenShots { get; private set; }
    public float reloadTime { get; set; }
    protected float loadTime;
    protected bool allowButtonHold;

    protected int damage;
    protected float spread;

    //-------------Bools-------------

    protected bool readyToShoot = true, reloading = false, shooting = false;
    protected int bulletsShot;

    public Weapon(WeaponStats stats, PlayerShooting playerShooting, InputManager inputManager) {
        if (stats == null) {
            Debug.LogWarning("Start stats missing!");
            return;
        }
        this.stats = stats;

        if(stats.bulletsTypes != null) {
            this.bulletTypes = stats.bulletsTypes;
        }
        
        this.playerShooting = playerShooting;
        this.inputManager = inputManager;

        //Set bullets left = magazine size in the derived constructor!
        // and reset stats
    }

    //-------------shoot cycle----------------
    //tryToShoot -> (shoot -> calculateShot -> finishShot -> PlayerShooting.Update/Shoot)* -> resetShot

    //tests, if weapon is in a state, that allows to initialize new shooting cycle
    public bool TryToShoot() {
        if (readyToShoot && !reloading) {
            return true;
        }
        return false;
    }

    //start shot
    //-> finish shot and return if its required to shoot again
    public bool Shoot() {

        readyToShoot = false;
        shooting = true;

        bulletsShot++;
        playerShooting.bulletsLeft.value--;  

        position = playerShooting.transform.position;

        StandardShot();

        return finishShot();
    }

    //Add new shooting methods here
    public abstract void StandardShot();


    //called via onStartLoading event when loadTime > 0 and InputManager.StartLoad() is called
    public virtual void OnStartLoading() {}
    //called via onEndLoading event when loadTime > 0 and InputManager.ResetLoad() or InputManager.FinishLoad() is called
    public virtual void OnEndLoading() {}

    //called when shootingInputUp gets true
    //possibility to tie to an event
    public virtual void OnStopShooting() {}

    private bool finishShot() {
        // reset shot if all bullets for the tap were fired or there are no bullets left
        // else shoot again
        if (bulletsShot >= bulletsPerTap || playerShooting.bulletsLeft.value < 1) {
            shooting = false;
            bulletsShot = 0;
            return false;
        } else {
            return true;
        } 
    }

    //called by PlayerShooting if Shoot returns false
    public void ResetShot() {
        readyToShoot = true;
    }

    //---------------reload cycle----------------------
    //tryReload -> PlayerShooting.reload() -> this.reload()

    public bool TryReload() {

        if (!reloading) {
            reloading = true;
            return true;
        }

        return false;
    }

    //consumes one ammo to reset bullets left to magazineSize
    public void Reload() {
        reloading = false;
        playerShooting.bulletsLeft.value = playerShooting.magazineSize.value;
        playerShooting.ammoCount.value--;
    }


    //--------------------help methods--------------------------

    protected void ResetStats()
    {
        damage = stats.damage;
        playerShooting.magazineSize.value = stats.magazineSize;
        bulletsPerTap = stats.bulletsPerTap;
        shootingRate = stats.shootingRate;
        timeBetweenShots = stats.timeBetweenShots;
        reloadTime = stats.startReloadTime;
        bulletSpeed = stats.bulletSpeed;
        spread = stats.spread;
        allowButtonHold = stats.allowButtonHold;
        spread = stats.spread;
        loadTime = stats.loadTime;

        bulletSize = stats.bulletSize;
    }

    //calculates the direction of the mouse to the player distorted by spreadToAdd
    protected Vector2 GetDirection(float spreadToAdd) {
        Vector2 direction = inputManager.mousePosition - position;
        direction = DirectionTools.Rotate(direction, Random.Range(-spreadToAdd, spreadToAdd)).normalized;
        return direction;
    }

    //spawns bullet from given type into the given direction
    protected Bullet SpawnBullet(Vector2 dir, BulletType type, float size) {
        
        GameObject bullet = ObjectPooler.Instance.SpawnFromPool(type.key, position, Quaternion.identity);

        if (bullet == null) {
            Debug.LogWarning("Creating bullet failed.");
            return null;
        }

        bullet.transform.localScale = new Vector3(size, size, 1);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(damage);
        bulletScript.SetNotHostile();

        bulletScript.SetDirection(dir * bulletSpeed);

        return bulletScript;
    }

    //debug method to print out the current weapons stats and mods
    public void PrintWeapon() {
        string output = "Damage: " + damage + "\n"
            + "Spread: " + spread + "\n"
            + "ShootingRate: " + shootingRate + "\n"
            + "LoadTime: " + loadTime + "\n"
            + "Small Mods: ";

        Debug.Log(output);

    }

    //--------------getter and setter---------------------
    
    public bool IsAllowButtonHold() {
        return allowButtonHold;
    }

    public float GetLoadTime() {
        return loadTime;
    }
}
