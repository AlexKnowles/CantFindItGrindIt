using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModelScript : MonoBehaviour {
    public float speed = 10.0F;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        //detect touches        
        Boolean touchEventInProgress = Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary);

        if (touchEventInProgress || Input.GetKey(KeyCode.UpArrow))
        {
            //transform.position += (Vector3.forward * Time.deltaTime);
            Vector3 nextPosition = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, nextPosition, (Time.deltaTime * speed));

        }
	}
}
