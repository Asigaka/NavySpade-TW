using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartScreen : UIScreen
{
    [SerializeField] private PlayerScore playerScore;

    [Space]
    [SerializeField] private Button startGameBtn;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        startGameBtn.onClick.AddListener(OnStartGameBtnClick);
    }

    private void OnEnable()
    {
        scoreText.text =  "Best Score - " + playerScore.GetSavedPlayerScore().ToString();
    }

    private void OnStartGameBtnClick()
    {
        GameStateController.Instance.ChangeState(GameState.StartGame);
    }
}
