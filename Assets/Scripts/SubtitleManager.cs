using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;
    [SerializeField] private TextMeshProUGUI subtitlesText;
    private List<Subtitle> que;
    private string content;
    private float contTime;

    private void Awake()
    {
        instance = this;
        que = new List<Subtitle>();
    }

    private void Start()
    {
        //AddInQue(new Subtitle { msg = "Hellow", time = 5f });
        //AddInQue(new Subtitle { msg = "WORLD", time = 3f });
    }

    public void AddInQue(Subtitle subtitle)
    {
        que.Add(subtitle);
        CompleteQue();
    }

    private void CompleteQue()
    {
        if (que.Count > 0)
        {
            PrintSubtitles(que[0].msg, que[0].time);
        }
    }

    private void PrintSubtitles(string msg, float time)
    {
        content = msg;
        contTime = time;
        StartCoroutine(SetSubtitles());
    }

    private IEnumerator SetSubtitles()
    {
        subtitlesText.text = content;
        yield return new WaitForSeconds(contTime);
        subtitlesText.text = "";
        CompleteQue();
        if (que.Count > 0)
            que.RemoveAt(0);
    }
}

[System.Serializable]
public class Subtitle
{
    public string msg;
    public float time;
}