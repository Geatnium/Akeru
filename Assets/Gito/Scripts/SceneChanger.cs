using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    private bool isClicked;

    [SerializeField] private float fadeTime;
    [SerializeField] private Image black;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            black.DOFade(1.0f, fadeTime);
            DOVirtual.DelayedCall(fadeTime, () =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
            });
        }
    }
}
