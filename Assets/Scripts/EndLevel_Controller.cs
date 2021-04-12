using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel_Controller : MonoBehaviour
{
    public bool isEndLevel = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isEndLevel = true;
        }
    }
}
