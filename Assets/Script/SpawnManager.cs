using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public float spawnTime;
    private float spawnTimer;
    public Transform spawnPosition;

    [Header("Variabili da non toccare")]
    public int PotionSpawned;
    public int maxPotionSpawn;
    public int inGamePotions;
    public bool CanSpawn;
    public float speed;
    public int level;

    bool gameover;
	// Use this for initialization
	void Start () {
        maxPotionSpawn = 1;
        level = 1;
        CanSpawn = true;
        EventManager.OnPotionDestroy += PotionDestroy;
        EventManager.GameOver += GameOver;
    }
	
	// Update is called once per frame
	void Update () {
        if (CanSpawn && !gameover)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnTime)
            {
                spawnTimer = 0;
                IPotion pooledPotion = PoolManager.instance.GetPooledPotion();
                if (pooledPotion != null)
                {
                    pooledPotion.Spawn(spawnPosition.position, RandomizeType(),speed);
                    inGamePotions++;
                    PotionSpawned++;
                    if (inGamePotions >= maxPotionSpawn)
                    {
                        CanSpawn = false;
                        spawnTimer = spawnTime;
                    }
                    if (PotionSpawned >= 10)
                    {
                        PotionSpawned = 0;
                        speed += 1f;
                        level++;
                        if (level == 2 || level == 7)
                            maxPotionSpawn++;
                    }
                }
            }
        }
	}

    public void PotionDestroy(IPotion _potion)
    {
        inGamePotions--;
        if (inGamePotions == 0)
        {
            CanSpawn = true;
        }
    }

    PotionTypes RandomizeType()
    {
        int randomindex=Random.Range(0, 4);
        return (PotionTypes)randomindex;
    }

    void GameOver()
    {
        gameover = true;
    }
}
