using UnityEngine;
using UnityEngine.Events;

public class Crystal : MonoBehaviour, IDisappearance, ISpawnable
{
    [SerializeField] private int minTakenScore = 1;
    [SerializeField] private int maxTakenScore = 10;
    [SerializeField] private int takenHealth = 1;

    [Header("Spawn Settings")]
    [SerializeField] private int minSpawnPeriodicity;
    [SerializeField] private int maxSpawnPeriodicity;
    [SerializeField] private int startCount;
    [SerializeField] private int maxSpawnedCountOnMap;

    private UnityEvent onDissapeare;

    public UnityEvent OnDisappeare => onDissapeare;
    public int TakenScore { get => Random.Range(minTakenScore, maxTakenScore); }
    public int TakenHealth { get => takenHealth; }
    public float MinSpawnPeriodicity => minSpawnPeriodicity;
    public float MaxSpawnPeriodicity => maxSpawnPeriodicity;
    public int StartCount => startCount;
    public int MaxSpawnedCountOnMap => maxSpawnedCountOnMap;

    private void Awake()
    {
        onDissapeare = new UnityEvent();
    }

    private void OnEnable()
    {
        OnDisappeare?.Invoke();
    }

    private void OnDisable()
    {
        OnDisappeare?.Invoke();
    }
}
