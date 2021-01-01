using UnityEngine;


public class InteractableObject : MonoBehaviour
{
    public string interactMessage = "";

   　[Multiline] public string interactedMessage = "";

    public virtual void DoAction()
    {
        if (!string.IsNullOrEmpty(interactedMessage))
        {
            Helper.ShowSubject(interactedMessage);
        }
    }

    public virtual void ExceptionInteract() { }
}
