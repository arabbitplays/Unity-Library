using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    private const int PLAYER_LAYER = 7, ENEMY_LAYER = 8, WALL_LAYER = 9;

    private int damage;
    [SerializeField] private Vector2 direction;

    //contains all objects, that are colliding with the bullet but already got damaged
    private List<IDamagable> damagedObjects;

    private bool isHostile = true;
    protected bool destroyOnHit = true;
    

    private void Awake() {
        damagedObjects = new List<IDamagable>();
    }

    public void OnSpawn() {
        isHostile = true;
    }

    /*
    call calculateHit when collider is
    a) a wall
    b) the player und bullet is hostile
    c) an enemy and bullet is not hostile
    */
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == WALL_LAYER 
                || (isHostile && coll.gameObject.layer == PLAYER_LAYER) 
                || (!isHostile && coll.gameObject.layer == ENEMY_LAYER)) {
            CalculateHit(coll);
        }
    }

    //remove element from list when bullets leaves its collider
    private void OnTriggerExit2D(Collider2D coll) {
        damagedObjects.Remove(coll.gameObject.GetComponent<IDamagable>());
    }

    private void FixedUpdate() {        
        transform.Translate(direction * Time.deltaTime);
    }

    /*
    if hit is damagable -> doDamage 
    Dont destroy if doDamage returns false and add to damageObjects
    -> bullets dont hit multiple times
    -> bullets dont get destroyed it they already hit and haven't been destroyed there

    returns if object got damaged
    */
    protected virtual bool CalculateHit(Collider2D coll) {
        IDamagable objectToDamage = coll.gameObject.GetComponent<IDamagable>();

        if (objectToDamage != null) {

            if (damagedObjects.Contains(objectToDamage)) {
                return false;
            }

            //when DoDamage() returns false or when destroyOnHit = false the bullet should not be destroyed
            bool destroyBullet = objectToDamage.DoDamage(damage);
            if (!destroyBullet || !destroyOnHit) {
                damagedObjects.Add(objectToDamage);
            } else {
                gameObject.SetActive(false);
            }

            return true;
        }

        gameObject.SetActive(false);

        return false;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public void SetDirection(Vector2 dir) {
        direction = dir;
    }

    public void SetNotHostile() {
        isHostile = false;
    }
}
