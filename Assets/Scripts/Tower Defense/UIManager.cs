using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverMessage;
    private Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        this.gameOverText = this._gameOverMessage.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance != null) 
            this.GetComponent<Text>().text = Player.Instance.HP.ToString();

        if (Player.Instance != null && Player.Instance.gameOver)
        {
            this._gameOverMessage.SetActive(true);
            this.gameOverText.text = "YOU DIED";
        }
    }
}
