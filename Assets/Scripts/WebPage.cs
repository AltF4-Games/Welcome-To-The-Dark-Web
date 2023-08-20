using UnityEngine;

[CreateAssetMenu(fileName = "WebPage", menuName = "ScriptableObjects/WebPage", order = 1)]
public class WebPage : ScriptableObject
{
    public Sprite spr;
    public string domain;
    public int id;
    [HideInInspector]
    public string url;
    [Header("Unique Properties")]
    public string properties;
    public string owner;
    public Category category;
    public bool illegal;

    public enum Category
    {
        Any,
        Blog,
        Store,
        Forum,
        Other
    }
}
