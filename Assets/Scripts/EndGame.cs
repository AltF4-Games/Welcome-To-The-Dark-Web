using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject cutOut;
    [SerializeField] private GameObject blackImg;
    [SerializeField] private GameObject logo;

    private void Start()
    {
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        yield return new WaitForSeconds(2f);
        Subtitle sub01 = new Subtitle
        {
            msg = "???: Any last wishes?",
            time = 2f,
        };
        SubtitleManager.instance.AddInQue(sub01);
        yield return new WaitForSeconds(2f);
        Subtitle sub02 = new Subtitle
        {
            msg = "Who do you work for",
            time = 3f,
        };
        SubtitleManager.instance.AddInQue(sub02);
        yield return new WaitForSeconds(1f);
        cutOut.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(4.7f);
        Subtitle sub03 = new Subtitle
        {
            msg = "Guys...",
            time = 1f,
        };
        SubtitleManager.instance.AddInQue(sub03);
        yield return new WaitForSeconds(1f);
        blackImg.SetActive(true);
        yield return new WaitForSeconds(1f);
        logo.SetActive(true);
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
