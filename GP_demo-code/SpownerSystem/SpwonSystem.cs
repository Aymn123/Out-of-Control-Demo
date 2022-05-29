using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwonSystem : MonoBehaviour
{
    [SerializeField] Transform[] SpawnerCount;

    [SerializeField] GameObject zombieMele;
    [SerializeField] GameObject zombieRang;
    [SerializeField] GameObject zombieBoom;

    public int waveCount=0;
    public int enemyAmount=4;
    
    public static int currentEnemyAmount=0;

    int inc;
     public int StarthealthEnemy=0;

    [SerializeField]float timer;
    [SerializeField] float TimeToRespawner=1;

    bool CanNextWave=false;
    [SerializeField] UI_Handler uI_Handler;
    void Start()
    {
        SpawnerCount = new Transform[transform.childCount];
        for(int i = 0; i < SpawnerCount.Length; ++i)
        {
            SpawnerCount[i] = transform.GetChild(i);
        }
        StartWave();
    }
    void Update()
    {
        uI_Handler.UI_Waves(waveCount);
        currentWave();
        timer += Time.deltaTime;  
    }
    void StartWave()
    {
        incrementHealthEnemy(100);
        waveCount++;
        accessToEnemyHealth(StarthealthEnemy);
        currentEnemyAmount = enemyAmount;
        inc = enemyAmount;
    }
    void Spawner()
    {
        Instantiate(zombieMele, SpawnerCount[Random.Range(0, SpawnerCount.Length)].transform.position, SpawnerCount[Random.Range(0, SpawnerCount.Length)].transform.rotation);
    }
    void NextWave()
    {

        if (currentEnemyAmount <= 0)
        { 
            Debug.Log("------------------------------------------");
            incrementHealthEnemy(5);
            accessToEnemyHealth(StarthealthEnemy);
            enemyAmount += 2;
            currentEnemyAmount = enemyAmount;
            inc = enemyAmount;
            waveCount++;
        }
    }
    void currentWave()
    {
        
        if (timer >= TimeToRespawner)
        {
            if (inc > 0)
            {
                timer *= 0;
                Spawner();
                inc--; 
            }
            else
            {
                NextWave();
            }
        }
    }
   
    
    void incrementHealthEnemy(int health)
    {
        StarthealthEnemy += health;
    }

    void accessToEnemyHealth(int health)
    {
        zombieMele.GetComponent<ZombieMelee>().health = health;
    }




}
