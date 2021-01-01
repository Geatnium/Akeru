using UnityEngine;
using UnityEngine.Events;

public class ColliderEventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent callEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (callEvent != null)
            {
                callEvent.Invoke();
            }
        }
    }
}
