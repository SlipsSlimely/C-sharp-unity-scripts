using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    // Target Aspect ratio hardcoded at 4:3 at the moment
    private float targetAspect = (4.0f / 3.0f);

    // Get the current Aspect Ratio
    private float currentAspect;

    // amount to scale viewport height by
    private float scaleHeight;

    // Store the camera
    Camera camera;

    Rect rect;
    private float scaleWidth;

    // Start is called before the first frame update
    void Start()
    {
        currentAspect = ((float)Screen.width / (float)Screen.height);
        scaleHeight = currentAspect / targetAspect;
        camera = GetComponent<Camera>();

        // add letterbox if scaled height is less than current height
        if (scaleHeight < 1.0f) 
        {
            rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else 
        {
            scaleWidth = 1.0f / scaleHeight;
            rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

}
