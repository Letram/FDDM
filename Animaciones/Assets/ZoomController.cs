using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour {

    float zoomMin = 5;
    float zoomMax = 15;
    // Update is called once per frame
    void Update()
    {
        float fov = gameObject.GetComponent<Camera>().orthographicSize;
        float newFov = fov;
        if ((Input.GetAxis("Mouse ScrollWheel") != 0f) && (fov <= zoomMax && fov >= zoomMin)) // forward
        {
            newFov += Input.GetAxis("Mouse ScrollWheel");
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Clamp(newFov, zoomMin, zoomMax);
        }
    }
}
 
