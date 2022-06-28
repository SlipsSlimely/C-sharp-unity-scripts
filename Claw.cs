using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public Transform origin;
    public float speed = 4.5f;
    public Gun gun;
    public ScoreManager scoreManager;


    private Vector3 target;
    private int jewelValue = 10;
    private GameObject childObject;
    private LineRenderer lineRenderer;
    private bool hitJewel;
    private bool retracting;


    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, transform.position);
        if (transform.position == origin.position && retracting)
        {
            gun.CollectedObject();
            if (hitJewel)
            {
                scoreManager.AddPoints(jewelValue);
                hitJewel = false;
                speed = 4.5f;
            }
            Destroy(childObject);
            gameObject.SetActive(false);
            speed = 4.5f;
        }
    }

    public void ClawTarget (Vector3 pos)
    {
        target = pos;
    }

    void OnTriggerEnter (Collider other)
    {
        retracting = true;
        target = origin.position;

        if(other.gameObject.CompareTag("Jewel"))
        {
            hitJewel = true;
            childObject = other.gameObject;
            other.transform.SetParent(this.transform);

        }
        else if (other.gameObject.CompareTag("Rock"))
        {
            speed = 3f;
            childObject = other.gameObject;
            other.transform.SetParent(this.transform);
        }
    }
}
