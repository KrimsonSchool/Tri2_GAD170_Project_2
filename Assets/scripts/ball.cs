using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ball : MonoBehaviour
{
    float tmr;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        tmr += Time.deltaTime;

        if (tmr >= 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 250);

            if(transform.position == target.position)
            {
                FindAnyObjectByType<GameManager>().CaughtCrewmate();
                Destroy(gameObject);
            }
        }
    }
}
