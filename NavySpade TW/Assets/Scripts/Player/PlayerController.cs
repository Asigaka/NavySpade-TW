using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    private PlayerHealth health;

    public static PlayerController Instance;

    public PlayerHealth Health { get => health; }

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;

        health = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        health.OnDie.AddListener(Die);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollider(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollider(collision.gameObject);
    }

    private void OnCollider(GameObject colliderObj)
    {
        Crystal crystal = colliderObj.GetComponent<Crystal>();
        Enemy enemy = colliderObj.GetComponent<Enemy>();

        if (crystal)
        {
            PlayerScore.Instance.IncreaseScore(crystal.TakenScore);
            health.IncreaseHealth(crystal.TakenHealth);
            crystal.gameObject.SetActive(false);
        }
        else if (enemy)
        {
            health.Damage();
            enemy.gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        GameStateController.Instance.ChangeState(GameState.LoseGame);
        Destroy(gameObject);
    }
}
