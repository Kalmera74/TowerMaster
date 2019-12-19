using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{

    [SerializeField]
    private float _waveFrequency; // for determining when to send the next wave
    [SerializeField]
    private float _spawnRate; // for determining when to spawn next enemy in the wave
    [SerializeField]
    private Wave[] _waves;
    [SerializeField]
    private GameObject _player;
    private int _rand = 0;


    private int GetRandom(int min, int max)
    {
        int rand = Random.Range(min, max);

        while (rand == _rand)
            rand = Random.Range(min, max);
        _rand = rand;
        return rand;
    }

    private void Instantiator(GameObject enemy)
    {
        GameObject go = Instantiate(enemy, _player.transform.forward * 30, Quaternion.identity);
        go.transform.LookAt(_player.transform.forward);
        go.transform.Translate(Vector3.forward, _player.transform);
        go.transform.Translate(Vector3.left * GetRandom(-2,5));

    }
    public void StartSpawn(int w)
    {
        StartCoroutine(Spawner(w));
    }

    private IEnumerator Spawner(int wave)
    {
        foreach(Phase P in _waves[wave-1].Phases)
        {
            foreach(GameObject go in P.Enemies)
            {
                Instantiator(go);
                yield return new WaitForSeconds(_spawnRate);
            }
            yield return new WaitForSeconds(_waveFrequency);
        }
    }


        /*  private IEnumerator Spawner(int wave, int _currentRow)
          {

              switch (_currentRow)
              {
                  case 1:
                      foreach (GameObject w in _waves[wave-1].FirstRow)
                      {
                          Instantiator(w);
                          yield return new WaitForSeconds(_spawnRate);
                      }
                      yield return new WaitForSeconds(_waveFrequency);
                      StartCoroutine(Spawner(wave, ++_currentRow));
                      break;
                  case 2:
                      foreach (GameObject w in _waves[wave-1].SecondRow)
                      {
                          Instantiator(w);
                          yield return new WaitForSeconds(_spawnRate);
                      }
                      yield return new WaitForSeconds(_waveFrequency);
                      StartCoroutine(Spawner(wave, ++_currentRow));
                      break;

                  case 3:
                      foreach (GameObject w in _waves[wave-1].ThirdRow)
                      {
                          Instantiator(w);
                          yield return new WaitForSeconds(_spawnRate);
                      }
                      yield return new WaitForSeconds(_waveFrequency);
                      StartCoroutine(Spawner(wave, ++_currentRow));
                      break;
                  case 4:
                      Instantiator(_waves[wave-1].Boss);
                      StopCoroutine("Spawn");
                      break;

              }



          } */
    }
