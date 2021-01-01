using UnityEngine;

public class MonoBehaviourUpdate : MonoBehaviour
{
    public virtual void UpdateM()
    {
    }

    private void Awake()
    {
        UpdateManager.AddList(this);
    }
}