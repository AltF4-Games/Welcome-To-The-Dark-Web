using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofJumpscare : MonoBehaviour
{
    [SerializeField] private AudioClip scareSound;
    private GameObject satan;
    private bool isDone = false;

    private void Start()
    {
        satan = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player") && !isDone)
        {
            satan.AddComponent<Rigidbody>();
            AudioManager.instance.PlayAudio(scareSound, 1.0f);
            isDone = true;
            Destroy(satan, 5f);
            Destroy(gameObject, 5f);
        }
    }
}
