using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { StartMenu, StartGame, LoseGame}
public class GameStateController : MonoBehaviour
{
    [SerializeField] private GameState startState;
    [SerializeField] private GameState currentState;

    [Header("Components")]
    [SerializeField] private UIManager ui;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private PlayerScore playerScore;

    public static GameStateController Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        ChangeState(startState);
    }

    public void ChangeState(GameState to)
    {
        currentState = to;

        switch (currentState)
        {
            case GameState.StartMenu:
                ui.ToogleScreen(ScreenType.Start);
                break;

            case GameState.StartGame:
                playerScore.ResetScore();
                spawnController.SpawnPlayer();

                ui.ToogleScreen(ScreenType.Session);
                break;

            case GameState.LoseGame:
                playerScore.SavePlayerScore();

                ui.ToogleScreen(ScreenType.Lose);
                break;
        }
    }
}
