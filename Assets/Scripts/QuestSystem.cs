using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem instance;
    [HideInInspector] public bool currentlyInQuest = false;
    [HideInInspector] public int currentQuest = 0;
    [SerializeField] private TextMeshProUGUI questText;

    private void Awake()
    {
        instance = this;
    }

    public void GiveQuest(Quest q)
    {
        if (currentlyInQuest == false)
        {
            currentQuest = q.questID;
            questText.text = "Quest: " + q.description;
            currentlyInQuest = true;
        }
    }

    public void CompleteCurrentQuest(Quest q = null)
    {
        if(currentlyInQuest)
        {
            questText.text = "Quest: Null";
            currentlyInQuest = false;
            if (q != null) GiveQuest(q);
        }
    }
}

public class Quest
{
    public int questID;
    public string description;
}