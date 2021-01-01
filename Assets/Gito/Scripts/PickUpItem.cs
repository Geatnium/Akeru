using UnityEngine;

[RequireComponent(typeof(Item))]
public class PickUpItem : InteractableObject
{
    [SerializeField] private AudioClip pickUpClip;

    private void Start()
    {
        if (string.IsNullOrEmpty(interactMessage))
        {
            interactMessage = "拾う";
        }
    }

    public override void DoAction()
    {
        base.DoAction();
        AudioManager.PlayOneShot(pickUpClip);
        ItemManager.AddItem(GetComponent<Item>());
        //Helper.ShowSubject(interactedMessage);
        Destroy(gameObject);
    }
}
