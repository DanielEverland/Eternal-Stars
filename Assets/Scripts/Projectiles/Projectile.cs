using UnityEditor.MemoryProfiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private LayerMask collisionMask;
    [SerializeField]
    private Color color = Color.white;
    [SerializeField]
    private new Light light;
    [SerializeField]
    private float maxDistance = 600;
    [SerializeField]
    private Vector3 movementDirection = Vector3.forward;
    [SerializeField]
    private float travelHeight = 1;
    [SerializeField]
    private float damage = 20;

    private Vector3 StartPos;

    public void InitializeMouse(Vector3 startPos)
    {
        InitializeMouse(new Vector2(startPos.x, startPos.z));
    }
    public void InitializeMouse(Vector2 startPos)
    {
        Plane plane = new Plane(Vector3.up, Vector3.up * travelHeight);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;
        plane.Raycast(mouseRay, out distance);

        Vector3 posInWorld = mouseRay.origin + mouseRay.direction * distance;
        Vector2 direction = new Vector2()
        {
            x = startPos.x - posInWorld.x,
            y = startPos.y - posInWorld.z,
        }.normalized;

        InitializeDirection(startPos, direction);
    }
    public void InitializeDirection(Vector3 startPos, Vector2 direction)
    {
        InitializeDirection(new Vector2(startPos.x, startPos.z), direction);
    }
    public void InitializeDirection(Vector2 startPos, Vector2 direction)
    {
        Vector3 endPos = new Vector3()
        {
            x = startPos.x - direction.x,
            z = startPos.y - direction.y,
        };

        InitializePosition(startPos, endPos);
    }
    public void InitializePosition(Vector3 startPos, Vector3 endPos)
    {
        InitializePosition(new Vector2(startPos.x, startPos.z), endPos);
    }
    public void InitializePosition(Vector2 startPos, Vector3 endPos)
    {
        StartPos = new Vector3(startPos.x, travelHeight, startPos.y);
        transform.position = StartPos;

        Vector2 direction = new Vector2()
        {
            x = StartPos.x - endPos.x,
            y = StartPos.z - endPos.z,
        };

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + 180;

        Vector3 angles = transform.eulerAngles;
        angles.y = angle;
        transform.eulerAngles = angles;
    }
    private void Awake()
    {
        AssignDataToComponents();
    }
    private void Update()
    {
        Vector3 rotatedDirection = transform.TransformDirection(movementDirection.normalized);
        Vector3 delta = rotatedDirection * (speed * Time.timeScale) * Time.unscaledDeltaTime;

        CheckForCollision(delta);
        Move(delta);
        CheckForOutOfBounds();
    }
    private void CheckForCollision(Vector3 delta)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, delta.normalized, out hit, delta.magnitude, collisionMask))
        {
            if (IsValidTarget(hit.collider.gameObject))
            {
                CollideWithObject(hit.collider.gameObject);
            }
        }
    }
    private void CheckForOutOfBounds()
    {
        float distance = Vector3.Distance(StartPos, transform.position);

        if(distance >= maxDistance)
        {
            DestroyProjectile();
        }
    }
    protected virtual bool IsValidTarget(GameObject obj)
    {
        return true;
    }
    protected virtual void CollideWithObject(GameObject obj)
    {
        DamageTaker damageTaker = obj.GetComponent<DamageTaker>();
        if(damageTaker != null)
        {
            DamageHandler.DealDamage(obj, damage);
        }

        DestroyProjectile();
    }
    private void DestroyProjectile()
    {
        PlayModeObjectPool.Pool.ReturnObject(gameObject);
    }
    private void Move(Vector3 delta)
    {
        Vector3 newPosition = transform.position + delta;
        newPosition.y = travelHeight;

        transform.position = newPosition;
    }
    private void AssignDataToComponents()
    {
        if (light != null) light.color = color;

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Color[] colors = new Color[mesh.vertices.Length];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = color;
        }

        mesh.colors = colors;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;

        Vector3 rotatedDirection = transform.TransformDirection(movementDirection.normalized);
        Gizmos.DrawLine(transform.position, transform.position + rotatedDirection * speed);
    }
}
