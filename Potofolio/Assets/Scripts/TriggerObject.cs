using UnityEngine;

public abstract class TriggerObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Trigger(other.gameObject);
    }

    protected abstract void Trigger(GameObject entered);
}
