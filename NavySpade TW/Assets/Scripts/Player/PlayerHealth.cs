using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHearts = 3;
    [SerializeField] private float invulnerableTime = 3;

    private int currentHearts;
    private bool invulnerable;

    [HideInInspector] public UnityEvent OnDie;
    [HideInInspector] public UnityEvent OnHeartsChange;

    public int CurrentHearts { get => currentHearts; }

    private void Awake()
    {
        currentHearts = maxHearts;
    }

    public void Damage(int damage = 1)
    {
        if (invulnerable)
            return;

        currentHearts -= damage;
        OnHeartsChange.Invoke();

        if (currentHearts <= 0)
            OnDie.Invoke();

        EnableInvulnerability();
    }

    public void IncreaseHealth(int health)
    {
        currentHearts += health;

        if (currentHearts > maxHearts)
            currentHearts = maxHearts;

        OnHeartsChange.Invoke();
    }

    public void EnableInvulnerability()
    {
        if (!invulnerable)
        {
            invulnerable = true;

            Invoke(nameof(ResetInvulnerability), invulnerableTime);
        }
    }

    private void ResetInvulnerability()
    {
        invulnerable = false;
    }
}
