using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startWave = 0;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);


    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int i = startWave ; i< waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

        }
    }

    private IEnumerator SpawnAllEnemiesInWave( WaveConfig currentWaveConfig )
    {

        for (int enemyCount = 0; enemyCount < currentWaveConfig.getNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                        currentWaveConfig.getEnemyPrefab(), 
                        currentWaveConfig.getWayPoints()[0].transform.position, 
                        Quaternion.identity
                        );
            newEnemy.GetComponent<EnemyPathing>().setWaveConfig(currentWaveConfig);

            yield return new WaitForSeconds(currentWaveConfig.getTimeBetweenSpawns());
        }

    }
}
