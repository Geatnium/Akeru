using UnityEngine;

public class EntranceDoor : InteractableObject
{
    [SerializeField] private AudioClip lockedDoor;
    public override void DoAction()
    {
        if (Events.progress == Progress.OutExam)
        {
            Helper.ShowSubject("クソ...！開かない！！\n鍵を探さないと...");
            AudioManager.PlayOneShotBig(lockedDoor);
            Events.progress = Progress.EntranceLock;
        }
        else if(Events.progress == Progress.EntranceKey)
        {
            Events.progress = Progress.Ending;
            GameObject.FindWithTag("Events").GetComponent<Events>().Ending();
        }
    }
}
