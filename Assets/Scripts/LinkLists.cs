using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinkLists : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField text;
    [SerializeField] private List<WebPage> links = new List<WebPage>();
    private const string DOMAIN = ".qor";
    private string[] list = new string[12];

    private void Start()
    {
        links = links.OrderBy(x => UnityEngine.Random.value).ToList();
        CreateLinkList();
    }

    private void CreateLinkList()
    {
        for (int i = 0; i < links.Count; i++)
        {
            links[i].url = CreateRandomURL();
            AddDescription(i);
        }
        text.text = GenerateText();
    }

    public static string CreateRandomURL()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[16];
        var random = new System.Random(UnityEngine.Random.Range(0, 99999));

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars) + DOMAIN;
    }

    private string GenerateText()
    {
        var s = "";
        foreach (var item in list)
        {
            s += item + "\n";
        }
        return (s);
    }

    private void AddDescription(int i)
    {
        switch (links[i].domain)
        {
            // LIST 1
            default:
                list[i] = links[i].url;
                break;
            case "ancient-grounds.qor":
                list[i] = links[i].url + " -> Archive";
                break;
            case "lie-phone-project.qor":
                list[i] = links[i].url + " -> LIE PHONE Project";
                break;
            case "banvideogames.qor":
                list[i] = links[i].url + " -> Ban Video Games Forum";
                break;
        }
    }
}
