using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public GameObject player;

    private bool useable = true;

    public float waittime;

    private Teleporter teleporter2;

    void OnTriggerEnter(Collider other)
    {
        GameObject teleporter = GameObject.FindGameObjectWithTag("teleport");
        if (other.tag == "Enemy" || other.tag == "Power" || other.tag == "Untagged")
        {
            return;
        }

        if (other.tag == "Player" && useable == true)
        {
            player.transform.position = teleporter.transform.position;
            useable = false;
            teleporter2.useable = false;
        }

    }
    void Update()
    {
        if (useable == false)    
        {
            waittime += Time.deltaTime;
            if (waittime >= 5)
            {
                useable = true;
                teleporter2.useable = false;
            }
                
        }
    }
}
