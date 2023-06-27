using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CrewMate : MonoBehaviour
{
    public string name;
    public string favouriteHobby;
    public bool parasite;
    char[] chars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    GameManager gameManage;

    string targetHobby;
    float tmr;
    bool selected;

    // Start is called before the first frame update
    void Start()
    {
        gameManage = FindAnyObjectByType<GameManager>();

        int isParasite = Random.Range(0, 5);

        if (isParasite == 0){
            parasite = true;
        }

        name = "" + chars[Random.Range(0, 26)] + "" + chars[Random.Range(0, 26)] + "" + chars[Random.Range(0, 26)] + "" + chars[Random.Range(0, 26)] + "" + chars[Random.Range(0, 26)] + "" + chars[Random.Range(0, 26)];

        if (!parasite)
        {
            favouriteHobby = gameManage.hobbies[Random.Range(0, 5)];
        }
        else
        {
            favouriteHobby = gameManage.parasiteHobbies[Random.Range(0, 5)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parasite)
        {
            if (!selected)
            {
                tmr += Time.deltaTime;
                if (tmr >= 2)
                {
                    targetHobby = gameManage.hobbies[Random.Range(0, 5)];
                    tmr = 0;
                    selected = true;
                }
            }
            else
            {
                if (gameManage.globalTimer <= 0)
                {
                    for (int i = 0; i < gameManage.crewMates.Length; i++)
                    {
                        if (gameManage.crewMates[i] != null)
                        {
                            print(gameManage.crewMates[i].favouriteHobby + " -> " + targetHobby);
                            if (gameManage.crewMates[i].favouriteHobby == targetHobby)
                            {
                                print("KILLIN!!!");
                                Destroy(gameManage.crewMates[i].gameObject);
                                gameManage.crewMates[i] = null;
                                gameManage.noOfCrew -= 1;
                                transform.Rotate(0, 90, 0);
                            }
                        }
                    }
                }
            }
            
        }
    }

    private void OnMouseDown()
    {
        //print(name + " " + favouriteHobby + " -> " + targetHobby);
        //crewmateInfoPanel.SetActive(true);
        //crewmateInfoPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Name: " + name +"\nFavourite hobby: " + favouriteHobby;

        gameManage.cip();
        gameManage.selectedCrewmate = this;
    }
}
