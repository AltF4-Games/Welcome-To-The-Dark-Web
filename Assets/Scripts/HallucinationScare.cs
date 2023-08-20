using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallucinationScare : MonoBehaviour
{
    [SerializeField] private Light lights;
    [SerializeField] private Material mat;
    [SerializeField] private FadeScreen fade;
    [SerializeField] private AudioClip scareSound;
    [SerializeField] private Collider[] col;
    [SerializeField] private Rigidbody[] rb;
    [SerializeField] private SkinnedMeshRenderer[] renders;
    private bool isDone = false;

    private void Start()
    {
        lights.enabled = true;
        mat.EnableKeyword("_EMISSION");
        foreach (Collider c in col)
        {
            c.enabled = false;
        }
        foreach (Rigidbody r in rb)
        {
            r.isKinematic = true;
        }
    }

    public void TriggerScare()
    {
        if (isDone) return;
        lights.enabled = false;
        mat.DisableKeyword("_EMISSION");
        isDone = true;
        StartCoroutine(Resetto());

        foreach (Collider c in col)
        {
            c.enabled = true;
        }
        foreach (Rigidbody r in rb)
        {
            r.isKinematic = false;
        }
    }

    private IEnumerator Resetto()
    {
        AudioManager.instance.PlayAudio(scareSound, 1.0f);
        yield return new WaitForSeconds(1.5f);
        fade.FadeBlack(1f, 1f);
        yield return new WaitForSeconds(1f);
        lights.enabled = true;
        mat.EnableKeyword("_EMISSION");
        foreach (SkinnedMeshRenderer ren in renders) {
            ren.enabled = false;
        }
        foreach (Collider c in col) {
            Destroy(c);
        }
        fade.FadeBlack(0f, 1.5f);
        Subtitle sub = new Subtitle { msg = "That hallucination felt so real", time = 2f };
        SubtitleManager.instance.AddInQue(sub);
    }
}
