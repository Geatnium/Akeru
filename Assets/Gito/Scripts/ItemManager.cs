using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<Item> itemList;

    private void Start()
    {
        itemList = new List<Item>();
    }

    private void _AddItem(Item item)
    {
        itemList.Add(item);
        switch (item.id)
        {
            case "FlashLight":
                GameObject.FindWithTag("Player").GetComponent<FlashLight>().GetFlashLight();
                break;
            case "Map":
                GameObject.FindWithTag("Player").GetComponent<Map>().GetMap();
                break;
            case "Key_Entrance":
                Events.progress = Progress.EntranceKey;
                break;
            default:
                break;
        }
    }

    private void _RemoveItem(Item item)
    {
        itemList.Remove(item);
    }

    private bool _Search(string id)
    {
        foreach (Item item in itemList)
        {
            if (item.id == id)
            {
                return true;
            }
        }
        return false;
    }

    public static void AddItem(Item item)
    {
        GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>()._AddItem(item);
    }

    public static void RemoveItem(Item item)
    {
        GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>()._RemoveItem(item);
    }

    public static bool Search(string id)
    {
        return GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>()._Search(id);
    }
}
