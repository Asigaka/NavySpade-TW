using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private ScreenType type;

    public ScreenType Type { get => type; }
}
