using UnityEngine;

public class EventSender : MonoBehaviour
{
    private Events events;

    private void Start()
    {
        events = GameObject.FindWithTag("Events").GetComponent<Events>();
    }

    private void Send(string methodName)
    {
        events.Invoke(methodName, 0f);
    }
}
