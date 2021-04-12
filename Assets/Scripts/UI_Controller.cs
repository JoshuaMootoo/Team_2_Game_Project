using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] Slider healthBarSlider;
    [SerializeField] Image healthBar;
    [SerializeField] Image healthBarIcon;
    [SerializeField] Sprite[] healthBarIcons = new Sprite[3];
    [SerializeField] Color[] healthBarColours = new Color[3];

    [Header("Transform Icons")]
    [SerializeField] Image playerNormal;
    [SerializeField] Image playerBall;
    [SerializeField] Color[] formIconColours = new Color[2];

    [Header("Light Collectables")]
    [SerializeField] Image[] lightCollectables = new Image[3];
    [SerializeField] Image[] endLevelUI = new Image[3];
    [SerializeField] Color[] lightIconColours = new Color[2];

    [Header("Other")]
    [SerializeField] Game_Controller gameController;
    Player_Controller player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    private void Start()
    {
        //  Health Bar
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = player.maxHealth;
    }

    private void Update()
    {
        //  Health Bar
        healthBarSlider.value = player.currentHealth;
        if (player.currentHealth > (player.maxHealth / 2))
        {
            healthBar.color = healthBarColours[0];
            healthBarIcon.sprite = healthBarIcons[0];
        }
        if (player.currentHealth <= (player.maxHealth / 2))
        {
            healthBar.color = healthBarColours[1];
            healthBarIcon.sprite = healthBarIcons[1];
        }
        if (player.currentHealth <= (player.maxHealth / 10))
        {
            healthBar.color = healthBarColours[2];
            healthBarIcon.sprite = healthBarIcons[2];
        }

        //  Transform Icons
        if (player.isBall)
        {
            playerNormal.color = formIconColours[1];
            playerBall.color = formIconColours[0];
        }
        else
        {
            playerNormal.color = formIconColours[0];
            playerBall.color = formIconColours[1];
        }

        //  Light Collectables
        for (int i = 0; i < 3; i++)
        {
            if (gameController.lights[i] == true)
            {
                lightCollectables[i].color = lightIconColours[0];
                endLevelUI[i].color = lightIconColours[0];
            }
            else
            {
                lightCollectables[i].color = lightIconColours[1];
                endLevelUI[i].color = lightIconColours[1];
            }
        }
    }
}
