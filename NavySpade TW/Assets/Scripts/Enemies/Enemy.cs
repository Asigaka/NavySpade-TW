using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IDisappearance, ISpawnable
{
    [SerializeField] private float moveSpeed;

    [Header("Spawn Settings")]
    [SerializeField] private int minSpawnPeriodicity;
    [SerializeField] private int maxSpawnPeriodicity;
    [SerializeField] private int startCount;
    [SerializeField] private int maxSpawnedCountOnMap;

    private NavMeshAgent agent;
    private UnityEvent onDissapeare;

    public UnityEvent OnDisappeare => onDissapeare;
    public float MinSpawnPeriodicity => minSpawnPeriodicity;
    public float MaxSpawnPeriodicity => maxSpawnPeriodicity;
    public int StartCount => startCount;
    public int MaxSpawnedCountOnMap => maxSpawnedCountOnMap;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        onDissapeare = new UnityEvent();
    }

    private void Update()
    {
        if (agent.remainingDistance < 3)
        {
            agent.SetDestination(MapControler.Instance.GetRandomPosOnGround());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Crystal crystal = GetComponent<Crystal>();

        if (crystal)
        {
            crystal.gameObject.SetActive(false);
        }
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
