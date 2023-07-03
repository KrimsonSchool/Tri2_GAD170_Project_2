using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //float tmr;
    public GameObject ship;
    public GameObject caughtText;
    public GameObject crewmate;
    public CrewMate[] crewMates;
    public Transform[] crewPos;
    public int noOfCrew;

    public string[] hobbies;
    public string[] parasiteHobbies;


    public GameObject crewmateInfoPanel;
    public TMPro.TextMeshProUGUI cipText;

    public CrewMate selectedCrewmate;

    public int globalTimer;

    public TMPro.TextMeshProUGUI gTimerText;
    public int h;

    public int timerMax;


    bool full;
    // Start is called before the first frame update
    void Start()
    {
        globalTimer = timerMax;
        Application.targetFrameRate = 120;
        h = globalTimer * Application.targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        //tmr += Time.deltaTime;

        h -= 1;

        globalTimer = h / Application.targetFrameRate;
        gTimerText.text = "" + Mathf.Round(globalTimer);

        if (h < 0)
        {
            if (noOfCrew < 10)
            {
                caughtText.SetActive(false);
                GameObject shp = Instantiate(ship, new Vector3(Random.Range(-240, 120), Random.Range(10, 90), 1000), Quaternion.identity);
                shp.transform.eulerAngles = new Vector3(0, 180, 0);
                //tmr = 0;

                globalTimer = timerMax;
                h = globalTimer * Application.targetFrameRate;
            }
            else
            {
                full = true;
                for (int i = 0; i < crewMates.Length; i++)
                {
                    if (crewMates[i] == null)
                    {
                        CrewMate crew = Instantiate(crewmate, crewPos[i].position, Quaternion.identity).GetComponent<CrewMate>();
                        crewMates[i] = crew;

                        crew.transform.eulerAngles = new Vector3(0, 180, 0);
                        full = false;
                        return;
                    }
                }

                if (full)
                {
                    Camera.main.backgroundColor = Color.green;
                    print("You Win!!!");
                    caughtText.GetComponent<TMPro.TextMeshPro>().text = "You Win!!!";
                    gTimerText.text = "";
                    for (int i = 0; i < noOfCrew; i++)
                    {
                        print("crew no " + i + ".Name = " + crewMates[i].name + " hobby = " + crewMates[i].favouriteHobby);
                    }
                }
                else
                {
                    noOfCrew -= 1;
                }
            }
        }

        


        if (selectedCrewmate != null)
        {
            crewmateInfoPanel.SetActive(true);
            cipText.text = "Name: " + selectedCrewmate.name + "\nFavourite hobby: " + selectedCrewmate.favouriteHobby;
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            print(noOfCrew);
        }
    }

    public void CaughtCrewmate()
    {
        caughtText.SetActive(true);

        if (noOfCrew < crewMates.Length)
        {
            //print(crewMates[noOfCrew]);
            if (crewMates[noOfCrew] == null)
            {
                CrewMate crew = Instantiate(crewmate, crewPos[noOfCrew].position, Quaternion.identity).GetComponent<CrewMate>();
                crewMates[noOfCrew] = crew;
                crew.transform.eulerAngles = new Vector3(0, 180, 0);

                noOfCrew += 1;
            }
            else
            {
                for (int i = 0; i < crewMates.Length; i++)
                {
                    if (crewMates[i] == null)
                    {
                        CrewMate crew = Instantiate(crewmate, crewPos[i].position, Quaternion.identity).GetComponent<CrewMate>();
                        crewMates[i] = crew;

                        crew.transform.eulerAngles = new Vector3(0, 180, 0);

                        noOfCrew += 1;
                        return;
                    }
                }
            }
        }
    }
    public void cip()
    {
    }

    public void Fire()
    {
        Destroy(selectedCrewmate.gameObject);
        noOfCrew -= 1;
        ExitMenu();
    }
    public void ExitMenu()
    {
        crewmateInfoPanel.SetActive(false);
        selectedCrewmate = null;
    }
}
