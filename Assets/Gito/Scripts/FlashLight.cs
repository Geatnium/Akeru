using UnityEngine;

public class FlashLight : MonoBehaviourUpdate
{
    [SerializeField] private GameObject flashLight;
    [SerializeField] private AudioClip flashSwitch;

    [SerializeField] private GameObject flashLightUI;

    private bool isHave;

    public void GetFlashLight()
    {
        isHave = true;
        flashLightUI.SetActive(true);
    }

    public override void UpdateM()
    {
        if (Player.state == PlayerState.Event) return;
        if (!isHave) return;
        if (Input.GetButtonDown("Flash"))
        {
            flashLight.SetActive(!flashLight.activeSelf);
            AudioManager.PlayOneShot(flashSwitch);
        }
    }
}
