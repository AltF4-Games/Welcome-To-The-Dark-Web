using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Toilet_EE : MonoBehaviour
{
    public void EasterEgg()
    {
        Subtitle subtitle = new Subtitle
        {
            msg = "A toilet in the kitchen would be so convenient",
            time = 1.75f,
        };
        SubtitleManager.instance.AddInQue(subtitle);
    }
}
