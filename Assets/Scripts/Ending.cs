using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private RenderTexture renderTex;
    [SerializeField] private GameObject cameraT;
    [SerializeField] private GameObject adam;
    [SerializeField] private GameObject anonEnd;
    [SerializeField] private Fireplace firePlace;
    [SerializeField] private Anon anon;
    [SerializeField] private Light roomLight;
    [SerializeField] private DoorScript_ door;
    [SerializeField] private Transform target;
    [SerializeField] private FadeScreen fade;

    public void PlayButton(GameObject self)
    {
        Destroy(self);
        door.OpenDoor();
        anonEnd.SetActive(true);
        anonEnd.GetComponent<Animator>().SetTrigger("Wave");
        renderTex.width = 1280; renderTex.height = 615;
        img.color = Color.white;
        roomLight.color = Color.red;
        img.texture = renderTex;
        cameraT.SetActive(true);
        adam.SetActive(true);
        firePlace.gameObject.SetActive(false);
        anon.gameObject.SetActive(false);
        GameController.instance.cursorState(true, false);
        StartCoroutine(AnonSeq());
    }

    private IEnumerator AnonSeq()
    {
        yield return new WaitForSeconds(4.11f);
        LeanTween.move(anonEnd, target.position, 2f);
        yield return new WaitForSeconds(2f);
        fade.FadeBlack(1, .5f);
        yield return new WaitForSeconds(.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("END");
    }
}
