using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Browser : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image img;
    [SerializeField] private TMPro.TMP_InputField input;
    [SerializeField] private GameObject linkList1;
    [SerializeField] private GameObject linkList2;
    [SerializeField] private GameObject realRR;
    [SerializeField] private CameraControl cc;
    [SerializeField] public List<WebPage> pages = new List<WebPage>();

    private void Start()
    {

    }

    public bool LoadPage(string correctedURL)
    {
        cc.ToggleCamera(false);
        linkList1.SetActive(false);
        linkList2.SetActive(false);
        foreach (WebPage page in pages)
        {
            if(page.url == correctedURL)
            {
                img.sprite = page.spr;
                RectTransform rt = img.GetComponent(typeof(RectTransform)) as RectTransform;
                rt.sizeDelta = new Vector2(page.spr.rect.width, page.spr.rect.height);
                CheckForProperties(page);
                return true;
            }
        }
        DisplayErrorPage();
        return false;
    }

    public void ClearPages()
    {
        img.sprite = null;
        input.text = null;
        linkList1.SetActive(false);
        linkList2.SetActive(false);
    }

    public void DisplayErrorPage()
    {
        img.sprite = pages[0].spr;
        RectTransform rt = img.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(pages[0].spr.rect.width, pages[0].spr.rect.height);
    }

    private void CheckForProperties(WebPage cache)
    {
        if (cache.properties == null) return;
        string[] properties = cache.properties.Split(' ');
        foreach (string property in properties)
        {
            if(property == "cc")
            {
                cc.ToggleCamera(true);
            }
            if (property == "LLI")
            {
                linkList1.SetActive(true);
            }
            if (property == "LLII")
            {
                linkList2.SetActive(true);
            }
            if (property == "RR")
            {
                realRR.SetActive(true);
            }
        }
    }
}
