using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RawImage LevelImage;
    public List<Texture> Texture_Stages;
    public Text LevelText;
    public Transform MenuPanel;
    public Text CoinText;

    public static UIManager instance;

    void Start() {
        instance = this;
    }

    public void SetStage(int stage) {
        LevelImage.texture = Texture_Stages[stage - 1];
    }

    public void SetLevel(string level) {
        LevelText.text = level;
    }

    public void SetCoin(string coin) {
        CoinText.text = coin;
    }

    public void OpenMenu() {
        MenuPanel.gameObject.SetActive(true);
    }

    public void CloseMenu() {
        MenuPanel.gameObject.SetActive(false);
    }
}
