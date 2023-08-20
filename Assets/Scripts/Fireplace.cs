using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [SerializeField] private Vector2 range;
    [SerializeField] private float countDownTime = 40f;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject stan;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private AudioClip extinguish;
    [SerializeField] private AudioClip matchStrike;
    [SerializeField] private AudioClip jumpscare;
    [SerializeField] private FadeScreen fadeScreen;
    [HideInInspector] public bool spawnStan = false;
    private bool fireStatus = true;

    public void LightFire()
    {
        fire.SetActive(true);
        AudioManager.instance.PlayAudio(matchStrike, 1.0f);
        if (GetComponent<Interactable>()) Destroy(GetComponent<Interactable>());
        fireStatus = true;
    }

    public IEnumerator RandomExtinguish()
    {
        float rng = Random.Range(range.x, range.y);
        yield return new WaitForSeconds(rng);
        fire.SetActive(false);
        AudioManager.instance.PlayAudio(extinguish, 1.0f, true, 20, transform.position);
        Interactable i = gameObject.AddComponent<Interactable>();
        i.id = "Light Fireplace";
        fireStatus = false;
        yield return new WaitForSeconds(countDownTime);
        if (!fireStatus)
        {
            spawnStan = true;
            yield break;
        }
        StartCoroutine(RandomExtinguish());
    }

    public IEnumerator KillPlayer()
    {
        stan.SetActive(true);
        LeanTween.move(playerCamera.gameObject, new Vector3(6.0002f, 3.945f, -2.535f), .5f);
        LeanTween.rotate(playerCamera.gameObject, new Vector3(-90f, 180, 0), .5f);
        yield return new WaitForSeconds(.5f);
        AudioManager.instance.PlayAudio(jumpscare, 1f);
        yield return new WaitForSeconds(.75f);
        fadeScreen.FadeBlack(1, .7f);
        //print("Using default scene Manager");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GAME");
    }
}
