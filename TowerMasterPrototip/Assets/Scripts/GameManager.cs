using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static Player Player;
    private bool _won = false;
    private bool _lost = false;
    private WaveGenerator _waveGenerator;
    private int _currentLevel;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        ShowMenu();
        DrawCoin();
    }

    private void ShowMenu()
    {

    }
    private void LoadNextLevel(int level)
    {

    }
    private void DrawLevelCount()
    {

    }
    private void DrawCoin()
    {

    }

}
