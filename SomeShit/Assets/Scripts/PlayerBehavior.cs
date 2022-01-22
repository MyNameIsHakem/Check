using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerBehavior : MonoBehaviour
{    
    [Tooltip("The Parent object of all the Tiles")]
    public GameObject Tiles;
    [Tooltip("The Button to role the Dice")]
    public GameObject RollingButton;
    [Tooltip("The text saying that the Dice is Rolling")]
    public TextMeshProUGUI WaitingText;
    [Tooltip("The Number of My Actual Tile")]
    public TextMeshProUGUI Score;
    [Tooltip("The Final Canvas To show in the end")]
    public GameObject EndDesplayer;

    //The spees of movement of the player it can be changed through setting    
    [Range(1, 10)] public static float Speed = 1;

    private bool DiceRolled = false;
    public static int TileIndex = 0;
    private int DiceNumber;

    private float Default_Y;
    private bool Positioned = false;
    private bool End = false;
    public static bool Finished = false;

    [Header("Events")]
    [Space]
    public UnityEvent Event;

    void Start()
    {       
        transform.position = Tiles.transform.GetChild(0).position + Vector3.up * 0.4f;

        Default_Y = transform.position.y;
    }

    void Update()
    {
        Score.text = $"Score : {TileIndex}";

        if (Input.GetButtonDown("Action") && RollingButton.activeSelf == true)
        {
            RollTheDice_EventHandler();
        }
        

        if (Input.GetButtonDown("Cancel"))
        {
            Event.Invoke();
        }


        Transform Current = Tiles.transform.GetChild(TileIndex);

        //if the dice is rolled i can start moving
        if (DiceRolled)
        {            
            //I start by going up 
            if(transform.position.y < Default_Y + 1.5f && Positioned == false)
            {
                transform.position += Vector3.up * Time.deltaTime * Speed;
            }
            else
            {
                WaitingText.text = $"You got : +{DiceNumber}";

                //Then i move forward till i arrive at The Current Tile position

                if (Mathf.Abs(transform.position.z) < Mathf.Abs(Current.position.z))
                {
                    transform.position += Vector3.forward * Time.deltaTime * Speed;
                }
                else
                {
                    //I set positioned to true to start landing on the tile

                    Positioned = true;
                }
              
            }

            if (Positioned)
            {
                //Here i will start landing

                if(transform.position.y > Default_Y)
                {
                    transform.position += Vector3.down * Time.deltaTime * Speed;
                }
                else
                {
                    Positioned = false;
                    DiceRolled = false;

                    RollingButton.SetActive(true);

                    WaitingText.enabled = false;
                    WaitingText.text = "The Dice is Rolling ...";
                }
            }

        }
        else
        {
            transform.position = Current.position + Vector3.up * 0.4f;
            Finished = true;
            if (TileIndex == Tiles.transform.childCount - 1)
                End = true;
        }

        if (End)
        {
            EndDesplayer.SetActive(true);
        }
    }
        
    public void RollTheDice_EventHandler()
    { 
        DiceNumber = Random.Range(1, 7);

        TileIndex += DiceNumber;

        if (TileIndex >= Tiles.transform.childCount - 1)
        {
            TileIndex = Tiles.transform.childCount - 1;            
        }
        

        DiceRolled = true;

        RollingButton.SetActive(false);

        WaitingText.enabled = true;
      
    }
}
