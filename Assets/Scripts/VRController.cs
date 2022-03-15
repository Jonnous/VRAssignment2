﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float m_Gravity = 30.0f;
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.5f;
    public float m_MaxRunSpeed = 3.0f;
    public float m_RotateIncrement = 45;

    public SteamVR_Action_Boolean m_RotatePress = null;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Boolean m_RunPress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_Speed = 0.6f;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    public Transform m_Head = null;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        //m_Head = SteamVR_Render.Top().head;
    }

    private void Update()
    {
        HandleHeight();
        CalculateMovement();
        SnapRotation();
    }

    private void HandleHeight()
    {
        //Get the head in local space
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        //cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        //Move capsule in local space
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        //apply
        m_CharacterController.center = newCenter;
    }

    private void CalculateMovement()
    {
        //Firgure out movement orientation
        Vector3 orientationEuler = new Vector3(m_Head.eulerAngles.x, m_Head.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        //If not moving
        if(m_MoveValue.axis.y == 0 && m_MoveValue.axis.x == 0)
        {
            m_Speed = 0;
        }

        //If button pressed
        if (m_MovePress.state)
        {
            if (m_RunPress.state)
            {
                //Add, clamp
                m_Speed += m_MoveValue.axis.y * (m_Sensitivity * 2);
                m_Speed = Mathf.Clamp(m_Speed, -m_MaxRunSpeed, m_MaxRunSpeed);

                //Orientation
                movement += orientation * (m_Speed * Vector3.forward);
            }
            else
            {
                //Add, clamp
                m_Speed += m_MoveValue.axis.y * m_Sensitivity;
                m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed);

                //Orientation
                movement += orientation * (m_Speed * Vector3.forward);
            }
        }



        //Gravity
        //movement.y -= m_Gravity * Time.deltaTime;

        //Apply
        m_CharacterController.Move(movement * Time.deltaTime);
    }

    private void SnapRotation()
    {
        float snapValue = 0.0f;

        if (m_RotatePress.GetLastStateDown(SteamVR_Input_Sources.LeftHand))
            snapValue = -Mathf.Abs(m_RotateIncrement);

        if (m_RotatePress.GetLastStateDown(SteamVR_Input_Sources.RightHand))
            snapValue = Mathf.Abs(m_RotateIncrement);

        transform.RotateAround(m_Head.position, Vector3.up, snapValue);
    }
}
