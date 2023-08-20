using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBox : MonoBehaviour
{
    private GameObject boxHands;
    private GameObject boxPrefab;
    private bool isInHands = false;
    private bool counted = false;

    private void Start()
    {
        boxHands = GameController.instance.boxHands;
        boxPrefab = GameController.instance.boxPrefab;
    }

    public void BoxInteractivity()
    {
        if(!boxHands.activeInHierarchy)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            boxHands.SetActive(true);
            isInHands = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (counted) return;
        if(collision.collider.transform.root.name == "ResidentialHouse")
        {
            counted = true;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SequenceManager>().CountBoxes();
            transform.parent = collision.collider.transform.root;
            Destroy(GetComponent<Interactable>());
            Destroy(this);
        }
    }

    private void Update()
    {
        if(isInHands)
        {
            if(Input.GetMouseButtonDown(KeyBinds.rmb))
            {
                boxHands.SetActive(false);
                GameObject box = Instantiate(boxPrefab, boxHands.transform.position, Quaternion.identity);
                box.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 10f,ForceMode.Impulse);
                isInHands = false;
                Destroy(gameObject);
            }
        }
    }
}
