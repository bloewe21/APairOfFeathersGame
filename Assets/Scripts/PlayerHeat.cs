using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : MonoBehaviour
{

    [SerializeField] private GameObject player;
    public PlayerMovement2 pm;
    public Image fillImage;
    private Slider slider;

    public bool increasing = false;
    private bool player_on_ground = false;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //set above player
        gameObject.transform.position = new Vector2(player.transform.position.x + .3f, player.transform.position.y + 0.8f);

        if (slider.value == slider.maxValue)
        {
            increasing = false;
            pm.freezeInputs();
        }
        else
        {
            if (Mathf.Abs(pm.rb.velocity.y) < .0001f)
            {
                player_on_ground = true;
            }
            else
            {
                player_on_ground = false;
            }
            if (!player_on_ground && pm.inHeat && !pm.isFrozen)
            {
                increasing = true;
            }
            else {
                increasing = false;
            }
        }

        if (increasing && slider.value <= slider.maxValue)
        {
            slider.value += Time.deltaTime;
        }
        else
        {
            slider.value -= (Time.deltaTime * 3.0f);
        }
        
    }
}
