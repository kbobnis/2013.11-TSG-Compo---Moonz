﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour {

    public float spawnCooldown;

    static float lastSpawnTime;

    static List<List<GameObject>> waves;

    public static int currentWaveId;
    public static int numEnemiesLeft;
    public static bool keepCalmAndWaitForNextWave;

    public void Start () {
        ExterminateTheWorld();
    }

    public static void ExterminateTheWorld() {
        lastSpawnTime = 0;
        InitWaves();
        currentWaveId = 0;
        numEnemiesLeft = waves[currentWaveId].Count;
        keepCalmAndWaitForNextWave = false;
    }

    static void InitWaves() {
        waves = new List<List<GameObject>>();

        CreateWave();
        AddEnemiesToWave("Zombie1", 10);

        CreateWave();
        AddEnemiesToWave("Zombie1", 20);

        CreateWave();
        AddEnemiesToWave("Zombie1", 25);
        AddEnemiesToWave("ZombieArcher", 3);

        CreateWave();
        AddEnemiesToWave("Zombie1", 30);
        AddEnemiesToWave("ZombieArcher", 10);

        CreateWave();
        AddEnemiesToWave("Zombie1", 20);
        AddEnemiesToWave("ZombieArcher", 15);
        AddEnemiesToWave("ZombieTaran", 5);

        CreateWave();
        AddEnemiesToWave("Zombie1", 25);
        AddEnemiesToWave("ZombieArcher", 25);
        AddEnemiesToWave("ZombieTaran", 15);

        CreateWave();
        AddEnemiesToWave("Zombie1", 25);
        AddEnemiesToWave("ZombieArcher", 20);
        AddEnemiesToWave("ZombieTaran", 20);
        AddEnemiesToWave("Boss", 1);

        CreateWave();
        AddEnemiesToWave("Zombie1", 25);
        AddEnemiesToWave("ZombieArcher", 10);
        AddEnemiesToWave("ZombieTaran", 25);
        AddEnemiesToWave("Boss", 3);

        CreateWave();
        AddEnemiesToWave("Zombie1", 25);
        AddEnemiesToWave("ZombieTaran", 30);
        AddEnemiesToWave("Boss", 6);

        CreateWave();
        AddEnemiesToWave("Zombie1", 40);
        AddEnemiesToWave("ZombieTaran", 3);
        AddEnemiesToWave("ZombieArcher", 8);
        AddEnemiesToWave("Boss", 8);
    }

    static void CreateWave() {
        waves.Add(new List<GameObject>());
    }

    static void AddEnemiesToWave(string kind, int num) {
        while (num-- > 0) {
            waves[waves.Count-1].Add( Resources.Load(kind, typeof(GameObject)) as GameObject );
        }
    }

    void NextWave() {
        if (currentWaveId != waves.Count - 1) {
            currentWaveId++;
            numEnemiesLeft = waves[currentWaveId].Count;
            keepCalmAndWaitForNextWave = false;
        }
    }

    void OnGUI() {
        if (keepCalmAndWaitForNextWave) {
            GUI.Label(new Rect(Screen.width*0.25f, Screen.height*0.25f, Screen.width*0.75f, Screen.height*0.75f), "<size=40>Get ready! Wave " + (currentWaveId+2)+"</size>");
        }
		int h = (int)(0.1f*Screen.height);
		int y = (int)(0.15f*Screen.height);
		GUI.Label(new Rect(0.01f*Screen.width, y, 0.3f*Screen.width, h), "Wave " + (currentWaveId+1));

		y += h;
        if (numEnemiesLeft >= 0) {
            GUI.Label(new Rect(0.01f*Screen.width, y, 0.3f*Screen.width, 0.1f*Screen.height), "Enemies Left: " + numEnemiesLeft);
        }
    }

    void Update () {
        if (numEnemiesLeft == 0) {
            // next waves
            Debug.Log("WaveDone");
            Invoke("NextWave", 5);
            numEnemiesLeft = -1;
            keepCalmAndWaitForNextWave = true;
        } else {
            if (waves[currentWaveId].Count > 0) {
                if (Time.time - lastSpawnTime > spawnCooldown) {
                    lastSpawnTime = Time.time;
                    int rnd = (int)(Random.value * waves[currentWaveId].Count);
                    GameObject prefab = waves[currentWaveId][rnd];
                    waves[currentWaveId].RemoveAt(rnd);

                    GameObject enemy = Instantiate(prefab) as GameObject;
                    enemy.GetComponent<SC>().SetPosition(-Camera.main.transform.position);
                    World.AddEnemy(enemy);
                }
            }
            SpreadEvilGuys();
        }
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
        numEnemiesLeft--;
        World.score += gameObject.GetComponent<Critter>().points;
    }
}
