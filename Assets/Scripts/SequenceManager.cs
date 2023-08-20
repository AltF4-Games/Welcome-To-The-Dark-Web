using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] private AudioClip doorSlam;
    [SerializeField] private GameObject laptop;
    private int boxes;

    private void Start()
    {
        AudioManager.instance.PlayAudio(doorSlam, 1.0f);
        Subtitle sub01 = new Subtitle { msg = "That was a long ride.", time = 3f };
        Subtitle sub02 = new Subtitle { msg = "I hate moving houses just because I accidentally leaked my IP", time = 3f };
        SubtitleManager.instance.AddInQue(sub01);
        SubtitleManager.instance.AddInQue(sub02);

        Quest moveBox = new Quest { questID = 0, description = "Move boxes inside" };
        QuestSystem.instance.GiveQuest(moveBox);
    }

    public void CountBoxes()
    {
        boxes++;
        if(boxes == 4) {
            Quest q = new Quest { questID = 1, description = "Get back to work" };
            QuestSystem.instance.CompleteCurrentQuest(q);
            Subtitle sub03 = new Subtitle { msg = "Alright, time to get back to work.", time = 2f };
            SubtitleManager.instance.AddInQue(sub03);
            Interactable i = laptop.AddComponent<Interactable>();
            i.id = "Laptop";
            boxes = -100;
        }
    }
}
