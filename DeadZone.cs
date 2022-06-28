using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    public AudioSource waterNoise;
    void OnTriggerEnter (Collider col)
    {
        waterNoise.Play();
        GM.instance.LoseLife();
    }
}
