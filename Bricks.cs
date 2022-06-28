using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public GameObject brickParticle;

    public AudioSource breakNoise;


    void OnCollisionEnter (Collision other)
    {

        Instantiate(brickParticle, transform.position, Quaternion.identity);
        breakNoise.Play();
        GM.instance.DestroyBrick();
        Destroy(gameObject);

        
    }

}
