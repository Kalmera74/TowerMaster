using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static Player Player;
    [SerializeField]
    private bool _won = false;

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
    [SerializeField]
    private Item[] _items;

    private static bool next = false;

    public static bool Next { set => next = value; }

    public static void Lost()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private bool _openShop = false;

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
        _waveGenerator.StartSpawn(level);
    }
    private void TurnPlayer(Transform player)
    {
        // Animate it, maybe coroutine, or animation?
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

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - (Screen.width / 2) * 0.32f, 0, 100, 25), "Gold: " + Player.GoldAmount))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                _openShop = false;
            }
            else
            {
                Time.timeScale = 0;
                _openShop = true;

            }
                
        }

        if (_openShop)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));

            // Draw a box in the new coordinate space defined by the BeginGroup.
            // Notice how (0,0) has now been moved on-screen
            GUI.Box(new Rect(0, 0, 800, 600), "SHOP");

            for(int i = 0; i < _items.Length; i++)
            {
                if (GUI.Button(new Rect(i * 160, 0, 150, 150), _items[i].name+"\n"+_items[i]._cost+" GOLD") && Player.GoldAmount >= _items[i]._cost)
                {
                    Player.SubstructGold(_items[i]._cost);
                    Player.GetComponent<Renderer>().material.mainTexture = _items[i]._skin;
                }
                   
            }

            if(GUI.Button(new Rect(250, 250, 150, 150), "CLOSE"))
            {
                _openShop = false;
                Time.timeScale = 1;
            }

            // We need to match all BeginGroup calls with an EndGroup
            GUI.EndGroup();
        }
    }

}
