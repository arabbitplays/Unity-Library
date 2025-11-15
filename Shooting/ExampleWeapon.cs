using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleWeapon : Weapon
{
    public ExampleWeapon(WeaponStats startStats, PlayerShooting playerShooting, InputManager inputManager) : base(startStats, playerShooting, inputManager) {
        name = "standard";

        playerShooting.bulletsLeft.value = playerShooting.magazineSize.value;
        ResetStats();
    }

    public override void StandardShot()
    {
        Vector2 direction = GetDirection(spread);

        SpawnBullet(direction, bulletTypes[0], bulletSize);
    }
}
