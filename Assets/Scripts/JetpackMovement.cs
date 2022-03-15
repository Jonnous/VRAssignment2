using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class JetpackMovement : MonoBehaviour
{
    private Rigidbody body;
    private SteamVR_TrackedObject controller;

    [SerializeField]
    private float thrustMultiplyer = 14f;

    [SerializeField]
    private float maxVelocity = 1f;

    private void OnEnable()
    {
        controller = GetComponent<SteamVR_TrackedObject>();
        body = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }
}
