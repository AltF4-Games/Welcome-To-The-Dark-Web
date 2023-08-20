using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image tutorial;
    [SerializeField] private GameObject settings;
    [SerializeField] private Sprite[] tutorialImgs;
    private int pageIndex = 0;

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GAME");
    }

    public void TutorialButton()
    {
        tutorial.gameObject.SetActive(!tutorial.gameObject.activeInHierarchy);
    }

    public void SettingsButton()
    {
        settings.gameObject.SetActive(!settings.gameObject.activeInHierarchy);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void TurnPage(bool fwd)
    {
        pageIndex = (fwd == true) ? Mathf.Clamp(++pageIndex,0,tutorialImgs.Length-1) : Mathf.Clamp(--pageIndex,0,tutorialImgs.Length-1);
        tutorial.sprite = tutorialImgs[pageIndex];
    }
}
