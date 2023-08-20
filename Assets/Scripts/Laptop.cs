using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;
using UnityEngine.UI;

public class Laptop : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Vector3 animFadeInVect;
    [SerializeField] private Vector3 animFadeInQuat;
    [SerializeField] private Vector2 minMaxVPN;
    [SerializeField] private CanvasGroup uiCanvas;
    [SerializeField] private CanvasGroup laptopCanvas;
    [SerializeField] private TextMeshProUGUI timeTextOS;
    [SerializeField] private TextMeshProUGUI timeTextIRL;
    [SerializeField] private Transform loadingGIF;
    [SerializeField] private GameObject VMOS;
    [SerializeField] private GameObject vpnTab;
    [SerializeField] private GameObject reportTab;
    [SerializeField] private GameObject searchTab;
    [SerializeField] private GameObject infoBox;
    [SerializeField] private GameObject satanScareTrig;
    [SerializeField] private Image padlock;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private Browser browser;
    [SerializeField] private CameraControl cc;
    [SerializeField] private Anon anon;
    [SerializeField] private Fireplace fireplace;
    [SerializeField] private DoorScript_ doorScript;
    [SerializeField] private TMP_InputField input;
    [HideInInspector] public bool isOn = false;
    private Vector3 cachedVect;
    private Vector3 cachedQuat;
    private bool firstTime = true;
    private int hour = 23; private int minute = 00;
    private bool vpnStatus = false;
    private string previousUrl = "deepWebP1-Wiki.qor";

    private void Update()
    {
        if(Input.GetMouseButtonDown(KeyBinds.lmb) && isOn)
        {
            AudioManager.instance.PlayAudio(clickSound, 1.0f);
        }
    }

    public void UseLaptop()
    {
        uiCanvas.alpha = 0;
        player.GetComponent<FirstPersonController>().enabled = false;
        playerCamera.parent = null;
        cachedVect = playerCamera.transform.position;
        cachedQuat = player.transform.rotation.eulerAngles;
        LeanTween.move(playerCamera.gameObject, animFadeInVect, 2f);
        LeanTween.rotate(playerCamera.gameObject, animFadeInQuat, 2f);
        if(firstTime) {
            Quest q = new Quest {questID = 2, description = "Report all available links" };
            QuestSystem.instance.CompleteCurrentQuest(q);
            fireplace.StartCoroutine(fireplace.RandomExtinguish());
            infoBox.SetActive(false);
            satanScareTrig.SetActive(true);
            firstTime = false;
        }
        Invoke("TurnOnLaptop", 3f);
    }

    private void TurnOnLaptop()
    {
        laptopCanvas.alpha = 1;
        laptopCanvas.blocksRaycasts = true;
        GameController.instance.cursorState(false, true);
        Invoke("CountTime", 45f);
        anon.StartCoroutine(anon.RandomAttack());
        doorScript.CloseDoor();
        isOn = true;
    }

    public void LeaveLaptop()
    {
        laptopCanvas.alpha = 0;
        laptopCanvas.blocksRaycasts = false;
        GameController.instance.cursorState(true, false);
        if(fireplace.spawnStan)
        {
            fireplace.StartCoroutine(fireplace.KillPlayer());
            return;
        }
        if (anon.canAttack == true)
        {
            anon.StartCoroutine(anon.KillPlayer());
            return;
        }
        foreach (var item in anon.mesh)
        {
            item.enabled = false;
        }
        LeanTween.move(playerCamera.gameObject, new Vector3(6.0002f, 3.945f, -2.237f), .9f);
        LeanTween.rotate(playerCamera.gameObject, new Vector3(0, -90, 0), .9f).setOnComplete(() =>
        {
            LeanTween.move(playerCamera.gameObject, cachedVect, 2f);
        });
        Invoke("TurnOffLaptop", 3f);
        anon.StopAllCoroutines();
        cc.ToggleCamera(false);
    }

    private void TurnOffLaptop()
    {
        uiCanvas.alpha = 1;
        player.GetComponent<FirstPersonController>().enabled = true;
        playerCamera.parent = player;
        isOn = false;
    }

    private void CountTime()
    {
        if (!isOn) return;
        minute += 10;
        if (minute == 60) 
        {
            minute = 00;
            hour++;
        }
        if (hour == 24)
        {
            hour = 00;
            minute = 00;
        }
        if (hour == 6)
        {
            hour = 5;
            minute = 59;
        }
        timeTextOS.text = hour + "h:" + minute + "m";
        timeTextIRL.text = hour + "h:" + minute + "m";
        Invoke("CountTime", 30f);
    }

    private void StopVPN()
    {
        padlock.color = Color.white;
        vpnStatus = false;
    }

    private IEnumerator FakeLoadForSeconds(int seconds)
    {
        loadingGIF.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        loadingGIF.gameObject.SetActive(false);
    }

    private void CheckUrl(string url)
    {
        if(!browser.LoadPage(url))
        {
            browser.DisplayErrorPage();
            print("Display err page");
        }
    }

    private void CheckVPN()
    {
        if(vpnStatus == false)
        {
            //print("Using default scene Manager");
            UnityEngine.SceneManagement.SceneManager.LoadScene("FEDS");
        }
    }

    public void OpenWindow(GameObject tab)
    {
        tab.SetActive(true);
    }

    public void CloseWindow(GameObject tab)
    {
        cc.ToggleCamera(false);
        tab.SetActive(false);
    }

    public void CloseVM(GameObject tab)
    {
        CloseWindow(tab);
        VMOS.SetActive(false);
    }

    public void ShutdownButton()
    {
        LeaveLaptop();
        StopVPN();
        VMOS.SetActive(false);
    }

    public void LoadVM()
    {
        StartCoroutine(FakeLoadForSeconds(5));
        Invoke("LoadOS",5);
    }

    private void LoadOS()
    {
        VMOS.SetActive(true);
    }

    public void ConnectVPN()
    {
        padlock.color = Color.green;
        CloseWindow(vpnTab);
        vpnStatus = true;
        float waitTime = Random.Range(minMaxVPN.x, minMaxVPN.y);
        Invoke("StopVPN", waitTime);
    }

    public void EnteredUrl()
    {
        if(input.text != null)
        {
            CheckUrl(input.text);
            CheckVPN();
        }
    }

    public void ReloadUrl()
    {
        browser.LoadPage(previousUrl);
        CheckVPN();
    }

    public void HomeButton()
    {
        browser.ClearPages();
        previousUrl = "";
    }

    public void SwitchTabs(string switcheroo)
    {
        switch (switcheroo) 
        {
            case "Report":
                reportTab.SetActive(true);
                searchTab.SetActive(false);
                break;
            case "Search":
                searchTab.SetActive(true);
                reportTab.SetActive(false);
                break;
        }
    }
}