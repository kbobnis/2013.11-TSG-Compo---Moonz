using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

    public float maxTotalEnemiesPower;
    public float maxEnemyPower;
    public float totalEnemiesPowerGain;
    public float enemyPowerGain;
    public float spawnCooldown;

    private static float totalEnemiesPower;
    float lastSpawnTime;
    ArrayList enemiesPrefabs;

    void Start () {
        totalEnemiesPower = 0;
        lastSpawnTime = 0;
        enemiesPrefabs = new ArrayList();
        enemiesPrefabs.Add(Resources.Load("Zombie1", typeof(GameObject)) as GameObject);
        enemiesPrefabs.Add(Resources.Load("ZombieTaran", typeof(GameObject)) as GameObject);
        enemiesPrefabs.Add(Resources.Load("ZombieArcher", typeof(GameObject)) as GameObject);
    }
    
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
                   enemy.GetComponent<SC>().SetPosition(-Camera.main.transform.position);
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
            GameObject nearest = World.GetNearestEnemy(sc);
            if (nearest != null) {
                SC otherSc = nearest.GetComponent<SC>();
                float distance = Vector3.Distance(sc.position, otherSc.position) - sc.radius - otherSc.radius;
                if (distance < 0) {
                    Vector3 direction = 0.125f * (otherSc.position - sc.position);
                    sc.position -= direction;
                    otherSc.position += direction;
                    sc.Correct();
                    otherSc.Correct();
                }
            }
		}
    }

	public static void EnemyDead(GameObject gameObject){
		ZombieSpawner.totalEnemiesPower -= gameObject.GetComponent<Critter>().power;
	}
}
