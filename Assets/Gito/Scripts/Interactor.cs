using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviourUpdate
{
    [SerializeField] private float rayDistance = 0.5f;

    [SerializeField] private GameObject interactUI;
    [SerializeField] private Text interactMessageText;

    private Transform mainCamera;

    private InteractableObject interactObj;

    private GameObject prevHitObject;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    public override void UpdateM()
    {
        if (Player.state == PlayerState.Event) return;
        Ray ray = new Ray(mainCamera.position, mainCamera.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.gameObject != prevHitObject || interactObj == null)
            {
                GameObject hitObj;
                ThrowInteractableObject throwInteractableObject = hit.collider.gameObject.GetComponent<ThrowInteractableObject>();
                if (throwInteractableObject != null)
                {
                    hitObj = throwInteractableObject.Throw();
                }
                else
                {
                    hitObj = hit.collider.gameObject;
                }
                InteractableObject hitInteractable = hitObj.GetComponent<InteractableObject>();
                interactObj = hitInteractable;
                if(interactObj != null)
                {
                    interactObj.ExceptionInteract();
                }
                prevHitObject = hit.collider.gameObject;
            }
        }
        else
        {
            interactObj = null;
        }

        if (interactObj == null)
        {
            if (interactUI.activeSelf)
            {
                interactUI.SetActive(false);
            }
        }
        else
        {
            if (!interactUI.activeSelf)
            {
                interactUI.SetActive(true);
            }
            if(interactObj.interactMessage != interactMessageText.text)
            {
                interactMessageText.text = interactObj.interactMessage;
            }
        }

        if (Input.GetButtonDown("Action"))
        {
            if (interactObj != null) interactObj.DoAction();
        }
    }
}
