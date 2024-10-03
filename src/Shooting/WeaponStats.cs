using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandardStats", menuName = "Stats/Standard", order = 1)]
public class WeaponStats : ScriptableObject
{
    public int damage, magazineSize, bulletsPerTap;
    public float shootingRate, timeBetweenShots, startReloadTime, bulletSpeed, spread, loadTime;
    public bool allowButtonHold;

    public BulletType[] bulletsTypes;
    public float bulletSize;
}
