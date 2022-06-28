using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDrop : MonoBehaviour
{
    private PlayerShooting playerController;
    public float timer;
    void OnTriggerEnter(Collider other)
    {
        GameObject playerControllerObject = GameObject.FindGameObjectWithTag("gun");
        if (other.tag == "Enemy" || other.tag == "Power" || other.tag == "Untagged")
        {
            return;
        }

        if (other.tag == "Player")
        {
            playerController = playerControllerObject.GetComponent<PlayerShooting>();
            if (playerController.timeBetweenBullets == .15f)
            {
                Destroy(gameObject);
                playerController.timeBetweenBullets = .07f;

            }
            else
                Destroy(gameObject);

        }

    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer == 10f)
        {
            timer = 0f;
            playerController.timeBetweenBullets = .15f;
        }
    }




}
