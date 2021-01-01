using UnityEngine;

public class Player : MonoBehaviour
{
    public static PlayerState state;

    private void Start()
    {
        state = PlayerState.Event;
    }
}

public enum PlayerState
{
    Freedom, Event, MouseLookOnly, Static, Map
}
