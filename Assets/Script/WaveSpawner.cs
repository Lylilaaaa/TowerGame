using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour
{
    public Transform startPosition;

    public GameObject enemyPrefab;

    public Text WaveTimer;

    public float timeBetweenWaves = 5f;
    private float countDown = 2f;

    private int waveNumber = 0;

    private void Update()
    {

        if (countDown <= 0)
        {
            StartCoroutine(spawnWave());

            countDown = timeBetweenWaves;
        }
        else
        {
            countDown -= Time.deltaTime;
        }

        WaveTimer.text = Mathf.Round(countDown).ToString();
    }

    IEnumerator spawnWave()
    {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemyPrefab,startPosition.position,startPosition.rotation);
    }
}
