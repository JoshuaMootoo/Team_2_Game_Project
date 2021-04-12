using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] GameObject textBox;
    UI_Controller UIController;
    private bool isActive = false;
    private bool canInteract;

    private void Awake()
    {
        UIController = GameObject.FindGameObjectWithTag("UI").GetComponent<UI_Controller>();
    }

    private void Update()
    {
        textBox.SetActive(isActive);
        if (canInteract && Input.GetButtonDown("Interact"))
        {
            isActive = !isActive;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInteract = false;
            isActive = false;
        }
    }
}
