using UnityEngine;

// INHERITANCE - Enemy is the parent class. Chaser, Shooter, and Tank derive from it.
public abstract class Enemy : MonoBehaviour
{
    private int m_Health = 3;
    private float m_Speed = 2f;
    private string m_EnemyName = "Enemy";

    protected Transform Target;

    // ENCAPSULATION - fields are private, access goes through properties with validation
    public int Health
    {
        get { return m_Health; }
        set { m_Health = Mathf.Max(0, value); }
    }

    public float Speed
    {
        get { return m_Speed; }
        set
        {
            if (value < 0f)
            {
                Debug.LogWarning("Speed can't be negative!");
                return;
            }
            m_Speed = value;
        }
    }

    public string EnemyName
    {
        get { return m_EnemyName; }
        set
        {
            if (value.Length > 12)
            {
                value = value.Substring(0, 12);
            }
            m_EnemyName = value;
        }
    }

    protected virtual float AttackInterval { get { return 1f; } }
    protected virtual Color BodyColor { get { return Color.white; } }

    private float m_NextAttackTime;

    protected virtual void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Target = player.transform;
        }
        GetComponent<Renderer>().material.color = BodyColor;
    }

    void Update()
    {
        TakeTurn();
    }

    // ABSTRACTION - one high-level method hides Move(), cooldown logic, and Attack()
    protected void TakeTurn()
    {
        if (Target == null) return;

        Move();

        if (Time.time >= m_NextAttackTime)
        {
            Attack();
            m_NextAttackTime = Time.time + AttackInterval;
        }
    }

    // POLYMORPHISM - children can override this (virtual)
    protected virtual void Move()
    {
        Vector3 direction = Target.position - transform.position;
        direction.y = 0f;
        transform.position += direction.normalized * Speed * Time.deltaTime;
    }

    // POLYMORPHISM - every child must provide its own version (abstract)
    protected abstract void Attack();

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, Target.position);
    }

    // POLYMORPHISM - method overloading: same name, different parameters
    public void TakeDamage(int amount)
    {
        Health -= amount;
        Debug.Log(EnemyName + " took " + amount + " damage. Health: " + Health);
        CheckDeath();
    }

    public void TakeDamage(int amount, bool isCritical)
    {
        int total = isCritical ? amount * 2 : amount;
        TakeDamage(total);
    }

    private void CheckDeath()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    // ABSTRACTION - callers just say Die(), they don't care how it happens
    protected virtual void Die()
    {
        Debug.Log(EnemyName + " is defeated!");
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        TakeDamage(1);
    }
}