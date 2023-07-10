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
        //sets the global timer variable to be equal to the timer max variable
        globalTimer = timerMax;
        //set the target framerate to 60
        Application.targetFrameRate = 60;
        //set the h variable to be equal to the global timer variable timesed by the targeted framerate
        h = globalTimer * Application.targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        //tmr += Time.deltaTime;

        //h is set to minus 1
        h -= 1;

        //global timer is set to h divided by the targeted framerate
        globalTimer = h / Application.targetFrameRate;
        //the timer text displays the rounded global timer variable
        gTimerText.text = "" + Mathf.Round(globalTimer);

        //if h is less than 0 then
        if (h < 0)
        {
            //if the number of crew is less than 10 then
            if (noOfCrew < 10)
            {
                //set the caught text to be inactive
                caughtText.SetActive(false);
                //spawn a new ship at a random  location
                GameObject shp = Instantiate(ship, new Vector3(Random.Range(-240, 120), Random.Range(10, 90), 1000), Quaternion.identity);
                //rotate the ship 180 so it's facing forward
                shp.transform.eulerAngles = new Vector3(0, 180, 0);
                //tmr = 0;

                //set the global timer to the timer max variable
                globalTimer = timerMax;
                //set h to be the global timer times the targeted framerate
                h = globalTimer * Application.targetFrameRate;
            }
            //if not then
            else
            {
                //set the variable full to be true
                full = true;
                //for every crewmate slot
                for (int i = 0; i < crewMates.Length; i++)
                {
                    //if the slot is empty then
                    if (crewMates[i] == null)
                    {
                        //spawn a crewmate in its slot position
                        CrewMate crew = Instantiate(crewmate, crewPos[i].position, Quaternion.identity).GetComponent<CrewMate>();
                        //add the crewmate to the slot
                        crewMates[i] = crew;

                        //rotate it 180 degrees so it's facing forward
                        crew.transform.eulerAngles = new Vector3(0, 180, 0);
                        //set the variable full to be false
                        full = false;
                        //exit out of the loop
                        return;
                    }
                }

                //if the full variable is true then
                if (full)
                {
                    //set the background colour to be green
                    Camera.main.backgroundColor = Color.green;
                    //print "You Win!!!" in the console
                    print("You Win!!!");
                    //set the caught text to display "You Win!!!"
                    caughtText.GetComponent<TMPro.TextMeshPro>().text = "You Win!!!";
                    //set the timer text to be blank
                    gTimerText.text = "";
                    //for every crewmate slot
                    for (int i = 0; i < noOfCrew; i++)
                    {
                        //print the number of the crewnmate, the name of the crewmember and the crewmembers favourite hobby in the console
                        print("crew no " + i + ".Name = " + crewMates[i].name + " hobby = " + crewMates[i].favouriteHobby);
                    }
                }
                //if not
                else
                {
                    //minus the number of crew by 1
                    noOfCrew -= 1;
                }
            }
        }

        

        //if the selected crewmate is not null then
        if (selectedCrewmate != null)
        {
            //set the infopanel to be active
            crewmateInfoPanel.SetActive(true);
            //set the infopanel text to display the crewmate's name and hobby
            cipText.text = "Name: " + selectedCrewmate.name + "\nFavourite hobby: " + selectedCrewmate.favouriteHobby;
        }


        //if the player presses the R key then
        if (Input.GetKeyDown(KeyCode.R))
        {
            //print the number of crew in the console
            print(noOfCrew);
        }
    }

    //the function CaughtCrewmate
    public void CaughtCrewmate()
    {
        //set the caught text to be active
        caughtText.SetActive(true);

        //if the number of crew is less than the amount of crewmate slots then
        if (noOfCrew < crewMates.Length)
        {
            //print(crewMates[noOfCrew]);
            //if the last crewmate is null then
            if (crewMates[noOfCrew] == null)
            {
                //spawn a new crewmate in its slot position
                CrewMate crew = Instantiate(crewmate, crewPos[noOfCrew].position, Quaternion.identity).GetComponent<CrewMate>();
                //add the crewmate to the slot
                crewMates[noOfCrew] = crew;
                //rotate the crewmate 180 degress so its facing forward
                crew.transform.eulerAngles = new Vector3(0, 180, 0);

                //add 1 to the number of crew
                noOfCrew += 1;
            }
            //if not then
            else
            {
                //for every crewmate slot
                for (int i = 0; i < crewMates.Length; i++)
                {
                    //if the crewmate slot is empty then
                    if (crewMates[i] == null)
                    {
                        //spawn a new crewmate in the slot position
                        CrewMate crew = Instantiate(crewmate, crewPos[i].position, Quaternion.identity).GetComponent<CrewMate>();
                        //add the crewmate to the slot
                        crewMates[i] = crew;

                        //rotate the crewmate 180 degrees to face forward
                        crew.transform.eulerAngles = new Vector3(0, 180, 0);

                        //add 1 to the number of crew
                        noOfCrew += 1;
                        //exit out of the loop
                        return;
                    }
                }
            }
        }
    }

    //the function fire
    public void Fire()
    {
        //destroy the currently selected crewmate
        Destroy(selectedCrewmate.gameObject);
        //minus 1 to the amount of crew
        noOfCrew -= 1;
        //call the exit menu function
        ExitMenu();
    }

    //the exit menu function
    public void ExitMenu()
    {
        //set the crewmate info panel to be inactive
        crewmateInfoPanel.SetActive(false);
        //set the selected crewmate to be null
        selectedCrewmate = null;
    }
}
