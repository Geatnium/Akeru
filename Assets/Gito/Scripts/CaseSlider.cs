using UnityEngine;
using DG.Tweening;

public class CaseSlider : InteractableObject
{
    public enum State
    {
        Open, Close, Lock, Static
    }

    public static float slideDulation = 0.5f;

    [SerializeField] private string thisKey;
    [SerializeField] private float openedPosition = -0.3f, closedPosition = -0.05f;

    [SerializeField] private AudioClip openClip, closeClip, lockClip, staticClip;

    public State state = State.Close;
    private bool isSliding;

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
                break;
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
                break;
            case State.Static:
                interactMessage = "開ける";
                break;
        }
    }

    private void Open()
    {
        if (isSliding) return;
        isSliding = true;
        AudioManager.PlayOneShot(openClip);
        transform.DOLocalMoveZ(openedPosition, slideDulation);
        DOVirtual.DelayedCall(slideDulation, () =>
        {
            isSliding = false;
            state = State.Open;
        });
    }

    private void Close()
    {
        if (isSliding) return;
        isSliding = true;
        AudioManager.PlayOneShot(closeClip);
        transform.DOLocalMoveZ(closedPosition, slideDulation);
        DOVirtual.DelayedCall(slideDulation, () =>
        {
            isSliding = false;
            state = State.Close;
        });
    }

    private void Unlock()
    {
        AudioManager.PlayOneShot(lockClip);

    }
}
