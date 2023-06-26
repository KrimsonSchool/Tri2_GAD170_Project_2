using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float tmr;
    public GameObject ship;
    public GameObject caughtText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmr += Time.deltaTime;

        if (tmr >= 10)
        {
            GameObject shp = Instantiate(ship, new Vector3(Random.Range(-240, 120), Random.Range(0, 90), 1000), Quaternion.identity);
            shp.transform.eulerAngles = new Vector3(0, 180, 0);
            tmr = 0;
        }
    }

    public void CaughtCrewmate()
    {
        caughtText.SetActive(true);
    }
}
