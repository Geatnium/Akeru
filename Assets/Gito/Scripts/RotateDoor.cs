using UnityEngine;
using DG.Tweening;

public class RotateDoor : InteractableObject
{
    public enum State
    {
        Open, Close, Lock, Static
    }

    public static float doorRotateDulation = 0.5f;

    [SerializeField] private MeshCollider[] meshColliders;
    [SerializeField] private string thisKey;
    [SerializeField] private float openedRotate = 90f;

    [SerializeField] private AudioClip openClip, closeClip, lockClip, staticClip, unlockClip;

    public State state = State.Close;
    private bool isRotating;

    private bool isUnlockable;

    private void Start()
    {

        if(meshColliders == null || meshColliders.Length == 0)
        {
            meshColliders = new MeshCollider[1];
            meshColliders[0] = GetComponent<MeshCollider>();
        }
        for(int i = 0; i < meshColliders.Length; i++)
        {
            meshColliders[i].convex = true;
        }
    }

    private void MeshCollidersEnable(bool enable)
    {
        for (int i = 0; i < meshColliders.Length; i++)
        {
            meshColliders[i].enabled = enable;
        }
    }

    public override void ExceptionInteract()
    {
        switch (state)
        {
            case State.Open:
                interactMessage = "閉じる";
                break;
            case State.Close:
                interactMessage = "開ける";
                break;
            case State.Lock:
                CheckKey();
                if(isUnlockable) interactMessage = "解錠";
                else　interactMessage = "開ける";
                break;
            case State.Static:
                interactMessage = "開ける";
                break;
        }
    }

    public override void DoAction()
    {
        switch (state)
        {
            case State.Open:
                Close();
                break;
            case State.Close:
                Open();
                break;
            case State.Lock:
                Unlock();
                break;
            case State.Static:
                AudioManager.PlayOneShot(staticClip);
                Helper.ShowSubject("この扉は開かない。");
                break;
        }
    }

    private void Open()
    {
        if (isRotating) return;
        isRotating = true;
        MeshCollidersEnable(false);
        AudioManager.PlayOneShot(openClip);
        transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x, openedRotate, transform.localRotation.eulerAngles.z), doorRotateDulation);
        DOVirtual.DelayedCall(doorRotateDulation, () =>
        {
            isRotating = false;
            state = State.Open;
            MeshCollidersEnable(true);
        });
    }

    private void Close()
    {
        if (isRotating) return;
        isRotating = true;
        MeshCollidersEnable(false);
        transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x, 0f, transform.localRotation.eulerAngles.z), doorRotateDulation);
        DOVirtual.DelayedCall(doorRotateDulation, () =>
        {
            AudioManager.PlayOneShot(closeClip);
            isRotating = false;
            state = State.Close;
            MeshCollidersEnable(true);
        });
    }

    private void CheckKey()
    {
        isUnlockable = ItemManager.Search(thisKey);
    }

    private void Unlock()
    {
        if (isUnlockable)
        {
            AudioManager.PlayOneShot(unlockClip);
            Helper.ShowSubject("鍵が開いた。");
            state = State.Close;
            interactMessage = "開ける";
        }
        else
        {
            AudioManager.PlayOneShot(lockClip);
            Helper.ShowSubject("鍵がかかっている。鍵を探そう。");
        }        
    }
}
