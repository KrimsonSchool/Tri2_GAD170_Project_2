using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject pokeBall;
    public int speed;
    float tmr;
    bool caught;

    float life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if (caught)
        {
            tmr += Time.deltaTime;

            if (tmr >= 0.3f)
            {
                Destroy(gameObject);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Caught();
        }

        life += Time.deltaTime;

        if (life >= 15)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        Caught();
        GetComponent<BoxCollider>().enabled = false;
    }

    public void Caught()
    {
        speed = 0;
        if (pokeBall != null)
        {
            GameObject ball = Instantiate(pokeBall, transform.position, Quaternion.identity);
            ball.transform.eulerAngles = new Vector3(90, 180, 0);
        }
        caught = true;
    }
}
