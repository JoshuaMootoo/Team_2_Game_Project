using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    public bool[] lights = new bool[3];
    [SerializeField] Light_Controller[] lightCollectables = new Light_Controller[3];
    [SerializeField] EndLevel_Controller endLevel;
    [SerializeField] GameObject endLevelUI;
    [SerializeField] Player_Controller player;

    private void Start()
    {
        player = FindObjectOfType<Player_Controller>();
    }

    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (lightCollectables[i].collected) lights[i] = true;
            else lights[i] = false;
        }

        if (endLevel.isEndLevel)
        {
            EndLevel();
        }
    }

    void EndLevel ()
    {
        endLevelUI.SetActive(true);
        player.rb2d.velocity = Vector3.zero;
    }
}
