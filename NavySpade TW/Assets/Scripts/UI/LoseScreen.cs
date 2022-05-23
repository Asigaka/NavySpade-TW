using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoseScreen : UIScreen
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    private PlayerScore score;

    private void Start()
    {
        restartBtn.onClick.AddListener(OnRestartBtnClick);
    }

    private void OnEnable()
    {
        score = PlayerScore.Instance;
        bestScoreText.text = "Best Score - " + score.GetBestPlayerScore().ToString();
        currentScoreText.text = "Current Score - " + score.CurrentScore.ToString();
    }

    private void OnRestartBtnClick()
    {
        GameStateController.Instance.ChangeState(GameState.StartGame);
    }
}
