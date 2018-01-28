using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CantFindItGrindIt
{
    public class ShiftGateReturner : MonoBehaviour
    {
        private float positionGap = 0.525f;

        private Vector3 initialPostion;
        private Rigidbody rigidbodyRef;
        private BoxCollider boxCollider;

        // Use this for initialization
        void Start()
        {
            initialPostion = transform.localPosition;
            rigidbodyRef = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.localPosition.y <= -0.3f)
            {
                Debug.Log(transform.localPosition.y);


                rigidbodyRef.useGravity = false;
                rigidbodyRef.velocity = Vector3.zero;
                rigidbodyRef.angularVelocity = Vector3.zero;
                boxCollider.enabled = false;

                transform.localRotation = new Quaternion(0, 0, 0, 0);
                transform.localPosition = new Vector3(0f, 0f, initialPostion.z + positionGap);

                initialPostion = transform.localPosition;
                Debug.Log(transform.localPosition.y);

                rigidbodyRef.useGravity = true;
                boxCollider.enabled = true;
            }
        }
    }
}