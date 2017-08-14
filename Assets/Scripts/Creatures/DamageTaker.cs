using UnityEngine.Events;
using UnityEngine;

public class DamageTaker : MonoBehaviour {

    public UnityEvent<float> OnDamge { get; set; }

    private void Awake()
    {
        OnDamge = new DamageTakerEvent();
    }
    public void TakeDamage(float damageAmount)
    {
        if (OnDamge != null)
            OnDamge.Invoke(damageAmount);
    }
    private void OnDestroy()
    {
        if (OnDamge != null)
            OnDamge.RemoveAllListeners();
    }

    private class DamageTakerEvent : UnityEvent<float>
    {

    }
}
