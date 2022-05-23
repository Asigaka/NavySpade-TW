using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenType { Start, Session, Lose}
public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIScreen> screens;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public void ToogleScreen(ScreenType toType)
    {
        foreach (UIScreen screen in screens)
        {
            screen.gameObject.SetActive(screen.Type == toType);
        }
    }
}
