using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI primaryText;
    [SerializeField] public GameObject indicator;
    [SerializeField] private float maxDistance = 2f;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            if (hit.transform.GetComponent<Interactable>() != null)
            {
                primaryText.text = hit.transform.GetComponent<Interactable>().id;
                indicator.SetActive(true);
                if (Input.GetKeyDown(KeyBinds.action))
                {
                    if (hit.transform.CompareTag("Toilet"))
                    {
                        hit.transform.GetComponent<Toilet_EE>().EasterEgg();
                    }
                    if (hit.transform.GetComponent<Interactable>().id == "Door" || hit.transform.GetComponent<Interactable>().id == "Drawer")
                    {
                        hit.transform.GetComponentInParent<DoorScript_>().ToggleDoor();
                    }
                    if (hit.transform.GetComponent<Interactable>().id == "Packaging Box")
                    {
                        hit.transform.GetComponent<PickupBox>().BoxInteractivity();
                    }
                    if (hit.transform.GetComponent<Interactable>().id == "Laptop")
                    {
                        hit.transform.GetComponent<Laptop>().UseLaptop();
                    }
                    if (hit.transform.GetComponent<Interactable>().id == "Light Fireplace")
                    {
                        hit.transform.GetComponent<Fireplace>().LightFire();
                    }
                    if (hit.transform.GetComponent<Interactable>().id == "Light Switch")
                    {
                        hit.transform.GetComponent<LightSwitch_>().ToggleLights();
                    }
                    if (hit.transform.GetComponent<Interactable>().id == "Electrical Panel")
                    {
                        hit.transform.GetComponent<PowerOutage>().ResetPower();
                    }
                }
            }
        }
        else
        {
            primaryText.text = "";
            indicator.SetActive(false);
        }
    }
}
