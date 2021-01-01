using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private static List<MonoBehaviourUpdate> _updates;
    private static List<MonoBehaviourUpdate> updates
    {
        get
        {
            return _updates == null ? _updates = new List<MonoBehaviourUpdate>() : _updates;
        }
    }

    public static void AddList(MonoBehaviourUpdate update)
    {
        updates.Add(update);
    }

    private void Update()
    {
        foreach (MonoBehaviourUpdate update in updates)
        {
            update.UpdateM();
        }
    }
}