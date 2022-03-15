using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class RayCast : MonoBehaviour
{
    public GameObject lazerPrefab;
    private GameObject lazer;

    private float orbitDist = 10;

    public PlanetProperties properties;

    public SteamVR_Action_Boolean pressTrigger;
    public SteamVR_Action_Boolean pressGrab;
    private bool triggerDown = false;
    private bool pickedUp = false;

    public GameObject planetEditMenu;
    public GameObject planetInfoMenu;

    public Text orbitSpeedText;
    public Text orbitDistText;
    public Text rotationSpeedText;
    public Text populationText;

    public bool isLeft;

    void UpdateLazerTransform(RaycastHit hit)
    {
        lazer.transform.position = Vector3.Lerp(this.transform.position, hit.point, 0.5f);
        lazer.transform.LookAt(hit.point);
        lazer.transform.localScale = new Vector3(0.01f, 0.01f, hit.distance);
    }

    // Start is called before the first frame update
    void Start()
    {
        lazer = Instantiate(lazerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if ((orbitSpeedText != null || orbitDistText != null || rotationSpeedText != null) && properties != null)
        {
            orbitSpeedText.text = "" + properties.orbitSpeed;
            orbitDistText.text = "" + properties.orbitDistance;
            rotationSpeedText.text = "" + properties.rotationSpeed;
            populationText.text = "" + properties.population;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            //Debug.Log("Ray is currently hitting" + hit.transform.name);
            if (hit.transform.CompareTag("floor"))
            {
                lazer.SetActive(false);
            }
            else
            {
                if (hit.transform.CompareTag("person"))
                {
                    Debug.Log("person");
                    if (pressGrab.GetStateDown(GetComponent<SteamVR_Behaviour_Pose>().inputSource))
                    {
                        Debug.Log("personDown");
                        hit.transform.SetParent(this.transform);
                        pickedUp = true;
                        hit.transform.GetComponent<Rigidbody>().isKinematic = pickedUp;
                    }

                    if (pressGrab.GetStateUp(GetComponent<SteamVR_Behaviour_Pose>().inputSource))
                    {
                        Debug.Log("personUp");
                        hit.transform.SetParent(null);
                        pickedUp = false;
                        hit.transform.GetComponent<Rigidbody>().isKinematic = pickedUp;
                    }
                }

                UpdateLazerTransform(hit);
                if (pressTrigger.state)
                {
                    Debug.Log("Ray is currently hitting" + hit.transform.name);
                    if (!triggerDown)
                    {
                        if (hit.transform.CompareTag("planet"))
                        {
                            Debug.Log("planet");
                            properties = hit.transform.GetComponent<PlanetProperties>();
                            triggerDown = true;
                        }

                        if (hit.transform.CompareTag("sun") && properties != null)
                        {
                            Debug.Log("sun");
                            properties.orbitDistance = orbitDist;
                            hit.transform.GetComponent<SunSpawn>().spawnPlanet(properties);
                            properties = null;
                            triggerDown = true;
                            orbitDist += 10;
                        }
                        if (properties != null && !isLeft)
                        {
                            switch (hit.transform.tag)
                            {
                                case "back":
                                    Debug.Log("back");
                                    if (planetEditMenu.activeSelf)
                                    {
                                        planetEditMenu.SetActive(false);
                                        planetInfoMenu.SetActive(true);
                                    }
                                    else
                                    {
                                        planetEditMenu.SetActive(true);
                                        planetInfoMenu.SetActive(false);
                                    }
                                    triggerDown = true;
                                    break;
                                case "scaleUp":
                                    Debug.Log("scaleUp");
                                    properties.scaleUp();
                                    triggerDown = true;
                                    break;
                                case "scaleDown":
                                    Debug.Log("scaleDown");
                                    properties.scaleDown();
                                    triggerDown = true;
                                    break;
                                case "waterToggle":
                                    Debug.Log("waterToggle");
                                    properties.activeWater();
                                    triggerDown = true;
                                    break;
                                case "mountainToggle":
                                    Debug.Log("mountainToggle");
                                    properties.activeMountain();
                                    triggerDown = true;
                                    break;
                                case "reset":
                                    Debug.Log("reset");
                                    properties.resetPlanet();
                                    triggerDown = true;
                                    break;
                                case "orbitSpeedUp":
                                    Debug.Log("orbitSpeedUp");
                                    properties.orbitSpeedUp();
                                    triggerDown = true;
                                    break;
                                case "orbitSpeedDown":
                                    Debug.Log("orbitSpeedDown");
                                    properties.orbitSpeedDown();
                                    triggerDown = true;
                                    break;
                                case "orbitDistUp":
                                    Debug.Log("orbitDistUp");
                                    properties.DistanceOut();
                                    triggerDown = true;
                                    break;
                                case "orbitDistDown":
                                    Debug.Log("orbitDistDown");
                                    properties.DistanceIn();
                                    triggerDown = true;
                                    break;
                                case "rotationSpeedUp":
                                    Debug.Log("rotationSpeedUp");
                                    properties.rotationSpeedUp();
                                    triggerDown = true;
                                    break;
                                case "rotationSpeedDown":
                                    Debug.Log("rotationSpeedDown");
                                    properties.rotationSpeedDown();
                                    triggerDown = true;
                                    break;
                                case "popUp":
                                    Debug.Log("popUp");
                                    properties.popUp();
                                    triggerDown = true;
                                    break;
                                case "popDown":
                                    Debug.Log("popDown");
                                    properties.popDown();
                                    triggerDown = true;
                                    break;
                            }
                        }

                    }
                }
                else { triggerDown = false; }
                lazer.SetActive(true);
            }
        }
        else
        {
            lazer.SetActive(false);
        }
    }


}
