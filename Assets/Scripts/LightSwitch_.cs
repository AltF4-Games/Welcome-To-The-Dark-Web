using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Interactable))]
public class LightSwitch_ : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private Material[] mats;
    [SerializeField] private AudioClip clack;

    public void ToggleLights()
    {
        AudioManager.instance.PlayAudio(clack, 1.0f);
        if(PowerOutage.instance.powerState)
        {
            foreach (Light light in lights)
            {
                light.enabled = !light.enabled;
            }
            if (lights[0].enabled)
            {
                foreach (Material mat in mats)
                {
                    mat.EnableKeyword("_EMISSION");
                }
            }
            else
            {
                foreach (Material mat in mats)
                {
                    mat.DisableKeyword("_EMISSION");
                }
            }
        }
    }
}
