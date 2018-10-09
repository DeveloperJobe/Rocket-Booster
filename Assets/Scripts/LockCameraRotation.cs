using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraRotation : MonoBehaviour {

    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject rocketShip;

    float newCameraPositionX, newCameraPositionY;
    float cameraPositionX, cameraPositionY;
	// Use this for initialization
	void Start ()
    {
        
    }

    void Update()
    {
        ChangeCameraPosition();
    }

    void ChangeCameraPosition()
    {
        mainCamera.transform.position = new Vector3(rocketShip.transform.position.x, rocketShip.transform.position.y, -37f);
    }
}
