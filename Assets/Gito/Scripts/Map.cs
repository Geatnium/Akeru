using UnityEngine;

public class Map : MonoBehaviourUpdate
{
    [SerializeField] private GameObject map;
    [SerializeField] private AudioClip paper;

    [SerializeField] private GameObject mapUI;

    private bool isHave;

    public void GetMap()
    {
        isHave = true;
        mapUI.SetActive(true);
    }

    public override void UpdateM()
    {
        if (Player.state == PlayerState.Freedom || Player.state == PlayerState.Map)
        {
            if (!isHave) return;
            if (Input.GetButtonDown("Map"))
            {
                if (Player.state == PlayerState.Freedom)
                {
                    Player.state = PlayerState.Map;
                    map.SetActive(true);
                }
                else if (Player.state == PlayerState.Map)
                {
                    Player.state = PlayerState.Freedom;
                    map.SetActive(false);
                }
                AudioManager.PlayOneShot(paper);
            }
        }
    }
}