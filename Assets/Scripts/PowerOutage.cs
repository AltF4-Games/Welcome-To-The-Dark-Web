using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOutage : MonoBehaviour
{
    public static PowerOutage instance;
    [SerializeField] private Vector2 range;
    [SerializeField] private Laptop laptop;
    [SerializeField] private AudioClip powerUp;
    [SerializeField] private AudioClip powerDown;
    [SerializeField] private Light[] lights;
    [SerializeField] private Material[] mats;
    [HideInInspector] public bool powerState = true;

    private void Start()
    {
        StartCoroutine(RandomOutage());
        instance = this;
        foreach (Material mat in mats)
        {
            mat.EnableKeyword("_EMISSION");
        }
    }

    private IEnumerator RandomOutage()
    {
        float rng = Random.Range(range.x, range.y);
        yield return new WaitForSeconds(rng);
        AudioManager.instance.PlayAudio(powerDown, .5f);
        powerState = false;
        Interactable i = gameObject.AddComponent<Interactable>();
        i.id = "Electrical Panel";
        Subtitle sub = new Subtitle
        {
            msg = "The previous owner warned me about this. I should flip the breaker. [Garage]",
            time = 2f
        };
        SubtitleManager.instance.AddInQue(sub);

        if(laptop.isOn == true)
        {
            laptop.LeaveLaptop();
        }
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
        foreach (Material mat in mats)
        {
            mat.DisableKeyword("_EMISSION");
        }
    }

    public void ResetPower()
    {
        if (GetComponent<Interactable>()) Destroy(GetComponent<Interactable>());
        powerState = true;
        AudioManager.instance.PlayAudio(powerUp, .5f);
        foreach (Light light in lights)
        {
            light.enabled = true;
        }
        foreach (Material mat in mats)
        {
            mat.EnableKeyword("_EMISSION");
        }
    }
}
