using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyBackground : MonoBehaviour {

    [SerializeField] GameObject backgroundObject;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = backgroundObject.transform.lossyScale;
	}
	
	// Update is called once per frame
	void Update () {
        WaveBackground();
	}

    void WaveBackground ()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSineWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        backgroundObject.transform.lossyScale.Set(startingPos.x + offset.x, startingPos.y + offset.y, startingPos.z + offset.z);
    }
}
