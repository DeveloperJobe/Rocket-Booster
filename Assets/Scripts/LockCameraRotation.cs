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
        cameraPositionY = mainCamera.transform.position.y;
        cameraPositionX = mainCamera.transform.position.x;
        newCameraPositionX = rocketShip.transform.position.x + mainCamera.transform.position.x;
        newCameraPositionY = rocketShip.transform.position.y + mainCamera.transform.position.y;
        mainCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
        cameraPositionX = newCameraPositionX;
        cameraPositionY = newCameraPositionY;
    }
}
