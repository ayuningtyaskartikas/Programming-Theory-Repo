using UnityEngine;

// INHERITANCE - Tank derives from Enemy
public class Tank : Enemy
{
    protected override float AttackInterval { get { return 2f; } }
    protected override Color BodyColor { get { return new Color(0.4f, 0.2f, 0.6f); } }

    void Awake()
    {
        EnemyName = "Tank";
        Health = 8;
        Speed = 1f;
        transform.localScale = Vector3.one * 1.5f;
    }

    // POLYMORPHISM - Tank hits slowly but hard
    protected override void Attack()
    {
        if (DistanceToTarget() < 2.5f)
        {
            Debug.Log("Tank slams for 3 damage!");
        }
    }
}