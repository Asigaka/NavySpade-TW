using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    public UnityEvent OnScoreChange;

    private const string playerPrefsKey = "player_score";
    [SerializeField] private int currentScore;

    public int CurrentScore { get => currentScore; }

    public static PlayerScore Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChange.Invoke();
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;
        OnScoreChange.Invoke();
    }

    public int GetBestPlayerScore()
    {
        if (CurrentScore > GetSavedPlayerScore())
            return CurrentScore;
        else
            return GetSavedPlayerScore();
    }

    public void SavePlayerScore()
    {
        if (CurrentScore > GetSavedPlayerScore())
            PlayerPrefs.SetInt(playerPrefsKey, CurrentScore);
    }

    public int GetSavedPlayerScore()
    {
        return PlayerPrefs.GetInt(playerPrefsKey);
    }
}
