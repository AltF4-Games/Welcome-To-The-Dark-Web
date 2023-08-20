using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject adam;
    [SerializeField] private GameObject cameraTarget;
    [SerializeField] private GameObject render;
    [SerializeField] private RenderTexture rt;

    private void Start()
    {
        rt.width = 450; rt.height = 250;
    }

    public void ToggleCamera(bool val)
    {
        adam.SetActive(val);
        cameraTarget.SetActive(val);
        render.SetActive(val);
    }
}
