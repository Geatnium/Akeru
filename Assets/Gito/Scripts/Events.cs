using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public enum Progress
{
    Exam, OutExam, EntranceLock, EntranceBack, InToile, EntranceKey, Ending
}

public class Events : MonoBehaviour
{
    [SerializeField] private GameObject toiletHuman;
    [SerializeField] private GameObject fallHuman;
    [SerializeField] private AudioClip fear;

    [SerializeField] private GameObject player, endingHuman;
    [SerializeField] private AudioClip endingHumanFootStep;
    [SerializeField] private GameObject endingUI;
    [SerializeField] private Image black;

    public static Progress progress = Progress.Exam;

    public void PlayerActivate()
    {
        Destroy(Camera.main.GetComponent<Animator>());
        Destroy(GameObject.Find("Main Camera/Mabuta"));
        Destroy(GameObject.Find("Main Camera/Bokeh"));
        Camera.main.transform.parent = GameObject.Find("Player/CrouchAxis/Neck").transform;
        Camera.main.transform.localPosition = new Vector3(0f, 0.15f, 0f);
        Player.state = PlayerState.Freedom;
        GameObject.FindWithTag("Helper").transform.Find("Gaze").gameObject.SetActive(true);
        GameObject.FindWithTag("Helper").transform.Find("How").gameObject.SetActive(true);
    }

    public void OutExam()
    {
        if(progress == Progress.Exam)
        {
            progress = Progress.OutExam;
            Helper.ShowSubject("こんなところ、さっさと出ないと...\n出口に向かおう。");
        }
    }

    public void EntranceFallHuman()
    {
        if(progress == Progress.EntranceLock)
        {
            progress++;
            fallHuman.SetActive(true);
            AudioManager.PlayOneShotBig(fear);
        }
    }

    public void ToileEvent()
    {
        if (progress == Progress.EntranceBack)
        {
            toiletHuman.SetActive(true);
        }
    }

    public void Ending()
    {
        StartCoroutine(EndingCor());
    }

    private IEnumerator EndingCor()
    {
        Player.state = PlayerState.Event;
        GameObject.FindWithTag("Helper").transform.Find("Gaze").gameObject.SetActive(false);
        GameObject.FindWithTag("Helper").transform.Find("Interact").gameObject.SetActive(false);
        GameObject.FindWithTag("Helper").transform.Find("How").gameObject.SetActive(false);
        GameObject.FindWithTag("Helper").transform.Find("Flash").gameObject.SetActive(false);
        GameObject.FindWithTag("Helper").transform.Find("Map").gameObject.SetActive(false);
        Helper.ShowSubject("やった！開いた！！");
        yield return new WaitForSeconds(1.0f);
        endingHuman.SetActive(true);
        endingHuman.transform.DOMove(player.transform.position, 3.25f).SetEase(Ease.Linear);
        IEnumerator footCor = HumanFoot();
        StartCoroutine(footCor);
        yield return new WaitForSeconds(2.0f);
        Helper.ShowSubject("ん？");
        player.transform.DOLookAt(endingHuman.transform.position, 0.5f);
        player.transform.Find("CrouchAxis/Neck").DOLocalRotate(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(1.0f);
        endingUI.SetActive(true);
        black.DOFade(1.0f, 0.2f);
        yield return new WaitForSeconds(0.25f);
        StopCoroutine(footCor);
        yield return new WaitForSeconds(2.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title2");
        yield return null;
    }

    private IEnumerator HumanFoot()
    {
        AudioSource audioSource = endingHuman.GetComponent<AudioSource>();
        while (true)
        {
            audioSource.PlayOneShot(endingHumanFootStep);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
