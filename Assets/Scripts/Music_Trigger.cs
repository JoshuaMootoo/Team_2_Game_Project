using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Trigger : MonoBehaviour
{
    private Music_Controller music_Controller;
    public AudioClip newTrack;

    private void Start()
    {
        music_Controller = FindObjectOfType<Music_Controller>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (newTrack != null) music_Controller.changeBGM(newTrack);
        }
    }
}
