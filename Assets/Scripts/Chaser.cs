using UnityEngine;

// INHERITANCE - Chaser derives from Enemy
public class Chaser : Enemy
{
    protected override float AttackInterval { get { return 0.8f; } }
    protected override Color BodyColor { get { return Color.red; } }

    void Awake()
    {
        EnemyName = "Chaser";
        Health = 2;
        Speed = 4f;
    }

    // POLYMORPHISM - Chaser attacks with a fast melee bite
    protected override void Attack()
    {
        if (DistanceToTarget() < 1.5f)
        {
            Debug.Log("Chaser bites for 1 damage!");
        }
    }
}