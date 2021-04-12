using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public bool[] lights = new bool[3];
    [SerializeField] Light_Controller[] lightCollectables = new Light_Controller[3];
    [SerializeField] EndLevel_Controller endLevel;

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

    }
}
