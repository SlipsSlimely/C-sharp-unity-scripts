using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        // This is a temporary soultion, need something smoother for rotations
        transform.Rotate(0, 0, speed);
    }
}