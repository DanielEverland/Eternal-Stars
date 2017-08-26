using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageTaker))]
[RequireComponent(typeof(CreatureQuadtreeReporter))]
public class Creature : MonoBehaviour {

    public CharacterController Controller { get { return _characterController; } }
    public float Speed { get { return _creatureData.Speed; } }
    public Rect Rect { get { return RecreateRect(); } }

    public float Health
    {
        get
        {
            return Data.Health - healthModifier;
        }
        set
        {
            healthModifier = Mathf.Clamp(Data.Health - value, 0, float.MaxValue);

            if(Health <= 0)
            {
                Die();
            }
        }
    }
    public float MaxHealth { get { return Data.Health; } }

    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private CreatureData _creatureData;
    
    protected CreatureData Data { get { return _creatureData; } }
    protected float actualSpeed { get { return (Speed * Time.timeScale) * Time.unscaledDeltaTime; } }
    protected Vector2 boundsCenter { get { return _creatureData.BoundsCenter; } }
    protected Vector2 boundsSize { get { return _creatureData.BoundsSize ; } }

    private float healthModifier;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        GetComponent<DamageTaker>().OnDamge.AddListener(TakeDamage);
    }
    protected virtual void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;
    }
    public void MoveToPosition(Vector3 positionToMoveTo)
    {
        Vector3 delta = positionToMoveTo - transform.position;
        Vector3 actualDelta = delta.normalized * Mathf.Clamp(delta.magnitude, 0, actualSpeed);

        Controller.Move(actualDelta);
    }
    public void MoveInDirection(Vector3 directionToMove)
    {
        Vector3 delta = directionToMove.normalized * actualSpeed;

        Controller.Move(delta);
    }
    private void RegisterAsCreature()
    {
        CreatureManager.Add(this);
    }
    private Rect RecreateRect()
    {
        Vector2 center = new Vector2()
        {
            x = transform.position.x + boundsCenter.x,
            y = transform.position.z + boundsCenter.y,
        };

        Vector2 size = Vector2.Scale(boundsSize, transform.lossyScale);

        return new Rect(center - size / 2, size);
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        if (_creatureData == null)
            return;

        Gizmos.color = Color.red;

        Rect rect = Rect;

        Vector3 size = new Vector3()
        {
            x = rect.width,
            z = rect.height,
        };

        Vector3 center = new Vector3()
        {
            x = rect.x + size.x / 2,
            z = rect.y + size.z / 2,
        };

        Gizmos.DrawWireCube(center, size);
    }
}
