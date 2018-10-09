using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class OscillatorScript : MonoBehaviour {

    [SerializeField] float period = 2f;
    [SerializeField] Vector3 movementVector;

    
    float movementFactor; // 0 for not moved, 1 for fully moved.
    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Oscillation();
    }

    private void Oscillation()
    {
        // Protect against period
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // Grows continually from 0
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSineWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
