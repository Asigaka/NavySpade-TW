using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SessionScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI crystalsCountText;
    [SerializeField] private TextMeshProUGUI enemiesCountText;
    [SerializeField] private TextMeshProUGUI nearestEnemyText;
    [SerializeField] private TextMeshProUGUI nearestCrystalText;
    [SerializeField] private GameObject heartPreafab;
    [SerializeField] private Transform heartsContainer;

    private PlayerController player;
    private PlayerScore score;

    private void OnEnable()
    {
        score = PlayerScore.Instance;
        score.OnScoreChange.AddListener(UpdatePlayerScoreText);

        if (PlayerController.Instance)
        {
            player = PlayerController.Instance;
            player.Health.OnHeartsChange.AddListener(UpdatePlayerHeartsCount);

            UpdatePlayerHeartsCount();
            UpdatePlayerScoreText();
        }
    }

    private void OnDisable()
    {
        if (player)
        {
            player.Health.OnHeartsChange.RemoveListener(UpdatePlayerHeartsCount);
        }
    }

    private void UpdatePlayerScoreText()
    {
        playerScoreText.text = score.CurrentScore.ToString();
    }

    private void UpdatePlayerHeartsCount()
    {
        for (int i = 0; i < heartsContainer.childCount; i++)
            Destroy(heartsContainer.GetChild(i).gameObject);

        for (int i = 0; i < player.Health.CurrentHearts; i++)
            Instantiate(heartPreafab, heartsContainer);
    }

    public void UpdateEnemiesAndCrystalsCount(int crystals, int enemies)
    {
        crystalsCountText.text = "Crystals - " + crystals.ToString();
        enemiesCountText.text = "Enemies - " + enemies.ToString();
    }

    public void SetNearestEnemy(int distance)
    {
        nearestEnemyText.text = "Nearest Enemy - " + distance;
    }

    public void SetNearestCrystal(int distance)
    {
        nearestCrystalText.text = "Nearest Crystal - " + distance;
    }
}
