using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Helper : MonoBehaviour
{
    [SerializeField] private Text subjectText;
    [SerializeField] private float fadeDulation = 0.5f, showDulation = 3.0f;

    private float showPositionY, closePositionY = -360;
    private Tween showingSubject;

    private void Start()
    {
        showPositionY = subjectText.rectTransform.localPosition.y;
        subjectText.rectTransform.localPosition = new Vector3(subjectText.rectTransform.localPosition.x, closePositionY, subjectText.rectTransform.localPosition.z);
    }

    private void ResetSubjectText()
    {
        subjectText.rectTransform.DOKill();
        subjectText.DOKill();
        subjectText.rectTransform.localPosition = new Vector3(subjectText.rectTransform.localPosition.x, closePositionY, subjectText.rectTransform.localPosition.z);
        subjectText.color = new Color(subjectText.color.r, subjectText.color.g, subjectText.color.g, 0f);
        if (showingSubject != null) showingSubject.Complete();
    }

    private void _ShowSubject(string message)
    {
        if (string.IsNullOrEmpty(message)) return;
        subjectText.text = message;
        ResetSubjectText();
        subjectText.rectTransform.DOLocalMoveY(showPositionY, fadeDulation);
        subjectText.DOFade(1.0f, fadeDulation);
        showingSubject = DOVirtual.DelayedCall(showDulation, () =>
        {
            subjectText.rectTransform.DOLocalMoveY(closePositionY, fadeDulation);
            subjectText.DOFade(0f, fadeDulation);
        });
    }

    public static void ShowSubject(string message)
    {
        GameObject.FindWithTag("Helper").GetComponent<Helper>()._ShowSubject(message);
    }
}
