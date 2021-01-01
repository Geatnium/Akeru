using UnityEngine;

public class ThrowInteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject interactableObj;

    public GameObject Throw()
    {
        return interactableObj;
    }
}
