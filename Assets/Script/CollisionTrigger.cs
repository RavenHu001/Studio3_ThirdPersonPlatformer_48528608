using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent getCoin = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            getCoin?.Invoke();
            Debug.Log("Get coin");
            GameObject parnent = transform.parent.gameObject;
            Destroy(parnent);
        }
    }
}
