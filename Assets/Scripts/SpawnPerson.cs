using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpawnPerson : MonoBehaviour
{

    public GameObject personPrefab;
    public SteamVR_Action_Boolean buttonPress;
    private bool isPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (buttonPress.state)
        {
            if (!isPressed)
            {
                GameObject temp = Instantiate(personPrefab);
                temp.transform.position = this.transform.position;
                isPressed = true;
            }

        }
        else
        {
            isPressed = false;
        }
    }
}
