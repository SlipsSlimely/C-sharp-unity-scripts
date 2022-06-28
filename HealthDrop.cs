using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{

    private PlayerHealth playerController;
    void OnTriggerEnter(Collider other)
    {
        GameObject playerControllerObject = GameObject.FindGameObjectWithTag("Player");
        if ( other.tag == "Enemy" || other.tag == "Power" || other.tag == "Untagged")
        {
            return;
        }

        if (other.tag == "Player")
        {
            playerController = playerControllerObject.GetComponent<PlayerHealth>();
            if (playerController.currentHealth <= 80)
            {
                Destroy(gameObject);
                playerController.currentHealth = playerController.currentHealth + 20;
                playerController.healthSlider.value = playerController.currentHealth;
            }   
            else
                Destroy(gameObject);

        }

    }





}
