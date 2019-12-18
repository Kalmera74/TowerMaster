using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static Player Player;
    [SerializeField]
    private bool _won = false;
    [SerializeField]
    private bool _lost = false;
    [SerializeField]
    private WaveGenerator _waveGenerator;
    [SerializeField]
    private int _currentLevel = 1;
    [SerializeField]
    private int _maxLevel = 4;
    [SerializeField]
    private static Object[] _enemyColors;
    [SerializeField]
    private static int _rand = 0;

    private static bool next = false;

    public static bool Next { set => next = value; }

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _enemyColors = Resources.LoadAll("Materials/EnemyColor", typeof(Material));

    }

    private void Update()
    {
        if (next)
        {
            if(_currentLevel < _maxLevel)
            {
                next = false;
                LoadNextLevel(++_currentLevel);
            }
            else
            {
                // Won
                Debug.Log("Won");   
            }


        }
    }
    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        LoadNextLevel(_currentLevel);
    }

    private void LoadNextLevel(int level)
    {
        Debug.Log("Level: " + level);
        TurnPlayer(Player.GetComponent<Transform>());
        _waveGenerator.StartSpawn(level, 1);
    }
    private void TurnPlayer(Transform player)
    {
        // Animate it, maybe coroutine?
        player.Rotate(0, 90, 0);
    }
    public static Material GetRandomEnemyColor(int min, int max)
    {

        int rand = Random.Range(min, max);

        while (rand == _rand)
            rand = Random.Range(min, max);
        _rand = rand;


        return (Material)_enemyColors[rand];
    }

}
