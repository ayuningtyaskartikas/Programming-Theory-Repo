using UnityEngine;

// INHERITANCE - Shooter derives from Enemy
public class Shooter : Enemy
{
    private const float KeepDistance = 6f;

    protected override float AttackInterval { get { return 1.5f; } }
    protected override Color BodyColor { get { return Color.cyan; } }

    void Awake()
    {
        EnemyName = "Shooter";
        Health = 3;
        Speed = 2.5f;
    }

    // POLYMORPHISM - Shooter stops and keeps its distance instead of rushing in
    protected override void Move()
    {
        transform.LookAt(new Vector3(Target.position.x, transform.position.y, Target.position.z));

        if (DistanceToTarget() > KeepDistance)
        {
            base.Move();
        }
    }

    // POLYMORPHISM - Shooter fires a projectile
    protected override void Attack()
    {
        GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.position = transform.position;
        ball.transform.localScale = Vector3.one * 0.3f;
        Destroy(ball.GetComponent<Collider>());

        Projectile projectile = ball.AddComponent<Projectile>();
        projectile.Direction = (Target.position - transform.position).normalized;
    }
}