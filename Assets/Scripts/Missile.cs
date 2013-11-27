using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Missile : MonoBehaviour {

    protected SC sc;
    protected SP sp;
	protected Sounds sounds; 
	public float speed;
    public float dmg;
    public float life;
    public string team;
    public float startingSpeed;


    void Start () {
        sc = GetComponent<SC>();
        sp = GetComponent<SP>();
    }

    void Update () {
        sc.MoveForward(speed * Time.deltaTime);

        List<GameObject> enemies = World.GetOppositeCollection(team);
        for (int i=enemies.Count-1; i>=0; --i) {
            GameObject enemy = enemies[i];
            if (sc.IsColliding(enemy)) {
                float overkill = enemy.GetComponent<Critter>().TakeDamage(dmg);
                if (overkill <= 0) {
                    World.RemoveMissile(gameObject);
                    return;
                }
            }
        }

        life -= Time.deltaTime;
        if (life < 0) {

            World.RemoveMissile(gameObject);
        }
    }

    public virtual void SetParamsFromWeapon(GameObject gameObjectWeapon) {
		Item weapon = gameObjectWeapon.GetComponent<Item>() as Item;
        speed = weapon.missileSpeed + startingSpeed;
        dmg = weapon.damage;
        life = weapon.maxDist / weapon.missileSpeed;
		sounds = gameObjectWeapon.GetComponent<Sounds>();
    }
}
