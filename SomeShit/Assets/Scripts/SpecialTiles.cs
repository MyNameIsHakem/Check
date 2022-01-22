using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpecialTiles : MonoBehaviour
{
    public GameObject Tiles, TextDisplayer , Player;
    private TextMeshProUGUI Text;

    private Transform current;

    void Start()
    {
        Text = TextDisplayer.GetComponent<TextMeshProUGUI>();

        for (int i = 1; i < Tiles.transform.childCount-1; i++)
        {
            Transform current = Tiles.transform.GetChild(i);

            if(Random.Range(0 , 2) > 0.5f)
            {
                int effect = Random.Range(0, 3);
                string Tag  = string.Empty;

                switch (effect)
                {
                    case 0:
                        Tag = "GoBack";
                        break;
                    case 1:
                        Tag = "Boost";
                        break;
                    case 2:
                        Tag = "Teleport";
                        break;
                }

                current.tag = Tag;
            }
            
        }
    }

    
    void Update()
    {
        current = Tiles.transform.GetChild(PlayerBehavior.TileIndex);

        if(PlayerBehavior.Finished == true)
        {
            if(current.tag == "Boost")
            {
                Debug.Log("You Got a boost");
                Player.GetComponent<PlayerBehavior>().RollTheDice_EventHandler();

                StartCoroutine(Show("Tou Got a boost of a few Tiles"));
            }
            else if(current.tag == "Teleport")
            {
                Debug.Log("You will go to a random tile");
                int Num = Mathf.Clamp(Random.Range(PlayerBehavior.TileIndex - 7, 
                                    Tiles.transform.childCount - 2) , 0 , Tiles.transform.childCount);

                StartCoroutine(Show($"You Teleported {Mathf.Abs(PlayerBehavior.TileIndex-Num)} " +
                     (PlayerBehavior.TileIndex < Num ? " Forward" : " Backward")));

                PlayerBehavior.TileIndex = Num;

                Player.transform.position = Tiles.transform.GetChild(PlayerBehavior.TileIndex).position 
                                                    + Vector3.up * 0.4f;

                
            }
            else if(current.tag == "GoBack")
            {
                Debug.Log("You Got a Pinelity");
                int num = Random.Range(1, 6);

                PlayerBehavior.TileIndex = Mathf.Clamp(PlayerBehavior.TileIndex - num, 
                                                                              0, Tiles.transform.childCount);

                Player.transform.position = Tiles.transform.GetChild(PlayerBehavior.TileIndex).position
                                                   + Vector3.up * 0.4f;

                StartCoroutine(Show($"Tou Got Back by {num} Tiles"));
            }

            PlayerBehavior.Finished = false;
        }
    }

    IEnumerator Show(string message)
    {
        Text.text = message;

        yield return new WaitForSeconds(1f);

        Text.text = "";
    }
}
