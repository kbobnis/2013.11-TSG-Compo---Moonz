using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

    public float maxTotalEnemiesPower;
    public float maxEnemyPower;
    public float totalEnemiesPowerGain;
    public float enemyPowerGain;
    public float spawnCooldown;

    float totalEnemiesPower;
    float lastSpawnTime;
    ArrayList enemiesPrefabs;

	// Use this for initialization
	void Start () {
        totalEnemiesPower = 0;
        lastSpawnTime = 0;
        enemiesPrefabs = new ArrayList();
        enemiesPrefabs.Add(Resources.Load("Zombie1", typeof(GameObject)) as GameObject);
	}
	
	// Update is called once per frame
	void Update () {
        maxTotalEnemiesPower += totalEnemiesPowerGain * Time.deltaTime;
        enemyPowerGain += enemyPowerGain * Time.deltaTime;

        if (Time.time - lastSpawnTime > spawnCooldown) {
            lastSpawnTime = Time.time;

            if (totalEnemiesPower < maxTotalEnemiesPower) {
               int index = (int)(Random.value * enemiesPrefabs.Count); 
               GameObject enemy = enemiesPrefabs[index] as GameObject;
               float power = enemy.GetComponent<Critter>().power;
               if (power < maxEnemyPower && totalEnemiesPower + power < maxTotalEnemiesPower) {
                   enemy = Instantiate(enemy) as GameObject;
                   World.AddEnemy(enemy);
                   totalEnemiesPower += power;
               }
            }
        }

        SpreadEvilGuys();
	}


    void SpreadEvilGuys() {
        for (int i=0; i<World.enemies.Count; ++i) {
            SC sc=World.enemies[i].GetComponent<SC>();
            SC otherSc = World.GetNearestEnemy(sc).GetComponent<SC>();
            float distance = Vector3.Distance(sc.position, otherSc.position) - sc.radius - otherSc.radius;
            if (distance < 0) {
                sc.SetDirectionTo(otherSc.position);
                otherSc.SetDirectionTo(sc.position);
                sc.MoveForward(distance*0.5f);
                otherSc.MoveForward(distance*0.5f);
            }
        }
    }
}
