using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
   
    [SerializeField]
    private float _waveFrequency;
    [SerializeField]
    private float _spawnRate;
    [SerializeField]
    private float _waveTime;
    [SerializeField]
    private int _currentWave;
    [SerializeField]
    private Wave[] _waves;
    public GameObject test;
    public Player _player;

    private void Start()
    {
        
        
      
        
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GameObject go = Instantiate(test, _player.transform.forward * 30, Quaternion.identity);

            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + Random.Range(-5, 5));
        }
    }


    // Make it Coroutine
    public void Spawn(int wave, GameObject[] spawnPoints)
    {
        foreach (GameObject w in _waves[wave].FirstRow)
            // Wait 
            Debug.Log(w.name);
        
    }
}
