using UnityEngine;

[CreateAssetMenu(fileName = "RadiusPatrolState.asset", menuName = "AI/States/Radius Patrol", order = Utility.CREATE_ASSET_ORDER_ID)]
public class RadiusPatrolState : AIState {

    [SerializeField]
    private float PatrolRadius = 50;
    [SerializeField]
    private float MinDistance = 5;

    private Vector3 targetPosition;

    public override void Initialize(EnemyBase owner, AIBrain brain)
    {
        base.Initialize(owner, brain);

        GetNewTarget();
    }
    public override void Update()
    {
        base.Update();
        
        float distance = Vector3.Distance(Transform.position, targetPosition);

        if (distance < MinDistance || distance > PatrolRadius)
            GetNewTarget();

        Vector3 delta = Transform.position - targetPosition;

        Owner.MoveInDirection(delta.normalized);        
    }
    private void GetNewTarget()
    {
        float angle = Random.Range(-180, 180);
        float distance = Random.Range(PatrolRadius / 2, PatrolRadius);
        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.right;

        targetPosition = Transform.position + direction.normalized * distance;
    }
}
