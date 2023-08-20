using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportDesk : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] private TMPro.TMP_InputField siteField;
    [SerializeField] private TMPro.TMP_Dropdown category;
    [SerializeField] private TMPro.TextMeshProUGUI ownerText;
    [SerializeField] private GameObject loading;
    [SerializeField] private Browser browser;
    [Header("Report")]
    [SerializeField] private TMPro.TMP_InputField siteFieldRP;
    [SerializeField] private TMPro.TMP_InputField ownerFieldRP;
    private List<WebPage> pages = new List<WebPage>();
    private List<WebPage> reportedSites = new List<WebPage>();
    private int successfulReports = 0;
    private int failedReports = 0;
    private const int maxSites = 7;
    private const int maxFails = 2;

    private void Start()
    {
        pages = browser.pages;
    }

    /*SEARCH*/

    public void SearchOwner()
    {
        foreach (WebPage page in pages)
        {
            if (siteField.text == page.url)
            {
                if (ParseCategory(category.value, page.category))
                {
                    StartCoroutine(ReturnAnswer(2f, page.owner));
                    return;
                }
                if (category.value == 0)
                {
                    StartCoroutine(ReturnAnswer(12f, page.owner));
                    return;
                }
                ownerText.text = "Result: Not a valid link";
            }
        }
    }

    private IEnumerator ReturnAnswer(float waitTime, string owner)
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        loading.SetActive(false);
        ownerText.text = "Result: " + owner;
    }

    private bool ParseCategory(int val, WebPage.Category cat)
    {
        int catVal = 0;
        switch (cat)
        {
            case WebPage.Category.Any:
                catVal = 0;
                break;
            case WebPage.Category.Blog:
                catVal = 1;
                break;
            case WebPage.Category.Store:
                catVal = 2;
                break;
            case WebPage.Category.Forum:
                catVal = 3;
                break;
            case WebPage.Category.Other:
                catVal = 4;
                break;
        }
        return (val == catVal);
    }

    /*REPORT*/

    public void ReportSite()
    {
        foreach (WebPage page in pages)
        {
            if (siteFieldRP.text == page.url)
            {
                foreach (WebPage report in reportedSites)
                {
                    if (page == report)
                    {
                        print(page.domain + report.domain);
                        SubmitReport(false);
                        return;
                    }
                }
                if (ownerFieldRP.text.ToLower() == page.owner.ToLower())
                {
                    if (page.illegal)
                    {
                        SubmitReport(true);
                        siteFieldRP.text = "";
                        ownerFieldRP.text = "";
                        reportedSites.Add(page);
                        return;
                    }
                }
            }
        }
        SubmitReport(false);
    }

    private void SubmitReport(bool val)
    {
        if(val == true)
        {
            successfulReports++;
            if(successfulReports == maxSites)
            {
                Mail m = new Mail
                {
                    accountName = "Anon",
                    subject = "",
                    content = "I'm really impressed with your snitching. Thanks to you my site is down.\n As an appreciation gift you can have a link to my redRoom. It's encrypted to fend off script kiddies.\n I hope you know how to use a terminal.\n" + Terminal.hash,
                };
                EMail.instance.AddMail(m);
            }
        }
        else
        {
            failedReports++;
            Mail m = new Mail
            {
                accountName = "SNITCH co",
                subject = "FALSE POSITIVE",
                content = "I think we made it very clear we don't like false positives. \n This is your final warning before we take action."
            };
            EMail.instance.AddMail(m);
            if (failedReports == maxFails)
            {
                //print("Using default scene Manager");
                UnityEngine.SceneManagement.SceneManager.LoadScene("GAME");
            }
        }
    }
}
