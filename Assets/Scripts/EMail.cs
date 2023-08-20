using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EMail : MonoBehaviour
{
	public static EMail instance;
	[SerializeField] private GameObject mailPanel;
	[SerializeField] private TextMeshProUGUI p_AccountText;
	[SerializeField] private TextMeshProUGUI p_SubjectText;
	[SerializeField] private TMP_InputField p_MailText;
	[SerializeField] private AudioClip notif;
	[SerializeField] private GameObject[] mailObject;
	[SerializeField] private TextMeshProUGUI[] accountText = new TextMeshProUGUI[3];
	[SerializeField] private TextMeshProUGUI[] subjectText = new TextMeshProUGUI[3];
	[SerializeField] private TextMeshProUGUI[] mailText = new TextMeshProUGUI[3];
	private Mail[] mCache = new Mail[3];
	private int mIndex = 0;

    private void Start()
    {
		instance = this;
        Mail m = new Mail { accountName = "ANON", subject = "Welcome to your new job", content = "Welcome to snitch club. We have a very strict policy, If you submit more than 2 false positives you will be fired & reported to FEDS" };
		AddMail(m, false);
    }

    /* BAD SCRIPT NEEDS POLISH*/
    public void AddMail(Mail m, bool notify = true)
	{
		mCache[mIndex] = m; 
		mailObject[mIndex].SetActive(true);
		accountText[mIndex].text = m.accountName;
		subjectText[mIndex].text = m.subject;
		mailText[mIndex].text = m.content;
		if(notify) AudioManager.instance.PlayAudio(notif, 1.0f);
		mIndex++;
	}

	public void RemoveMail()
	{
		foreach (GameObject mailObjects in mailObject)
		{
			mailObjects.SetActive(false);
		}
	}

	public void OpenMail(int mailIndex)
	{
		mailPanel.SetActive(true);
		p_AccountText.text = mCache[mailIndex].accountName;
		p_SubjectText.text = mCache[mailIndex].subject;
		p_MailText.text = mCache[mailIndex].content;
	}

	public void CloseMail()
	{
		mailPanel.SetActive(false);
		p_AccountText.text = "";
		p_SubjectText.text = "";
		p_MailText.text = "";
	}
}

public class Mail
{
	public string accountName;
	public string subject;
	public string content;
}
