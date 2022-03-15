using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MenuHide : MonoBehaviour
{
    public GameObject menu;
    public SteamVR_Action_Boolean button;

    // Update is called once per frame
    void Update()
    {
        if (button.state)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }
    }
}
