using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetProperties : MonoBehaviour
{
    public GameObject wholePlanet;
    public GameObject waterLayer;
    public GameObject mountainLayer;
    public GameObject fire;
    public GameObject ice;

    private Vector3 planetScale;

    private Vector3 defaultSetting;
    private bool defaultWater;
    private bool defaultMountain;

    public enum PLANET_TYPE{FREEZE, NORMAL, HOT}

    public float orbitSpeed;
    public float orbitDistance;
    public float rotationSpeed;
    public int population;
    public GameObject planetPrefab;

    public bool isTest;

    private void Start()
    {
        wholePlanet = this.gameObject;
        waterLayer = this.transform.Find("water").gameObject;
        mountainLayer = this.transform.Find("mountain").gameObject;
        planetScale = wholePlanet.transform.localScale;
        fire = this.transform.Find("fire").gameObject;
        ice = this.transform.Find("ice").gameObject;

        defaultSetting = wholePlanet.transform.localScale;
        defaultWater = waterLayer.activeSelf;
        defaultMountain = mountainLayer.activeSelf;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("person"))
        {
            popUp();
            Destroy(other.gameObject);
        }
    }

    public void setRotationSpeed(float speed)
    {
        if (!isTest) rotationSpeed = speed;
    }

    public void rotationSpeedUp()
    {
        if (!isTest) rotationSpeed += 1;
    }

    public void rotationSpeedDown()
    {
        if (!isTest) rotationSpeed -= 1;
    }

    public void setPopulation(int pop)
    {
        if (!isTest) population = pop;
    }

    public void popUp()
    {
        if (!isTest) population += 1;
    }

    public void popDown()
    {
        if (population > 0) if (!isTest) population -= 1;
    }

    public Vector3 getPlanetScale()
    {
        return planetScale;
    }

    public bool getWaterLayerActive()
    {
        return waterLayer.activeSelf;
    }

    public bool getMountainLayerActive()
    {
        return mountainLayer.activeSelf;
    }


    public void preset(Vector3 scale, bool water, bool mountain)
    {
        planetScale = scale;
        waterLayer.SetActive(water);
        mountainLayer.SetActive(mountain);
    }

    public void setOrbitSpeed(float speed)
    {
        if (!isTest) orbitSpeed = speed;
    }

    public void orbitSpeedUp()
    {
        if (!isTest) orbitSpeed += 2;
    }

    public void orbitSpeedDown()
    {
        if (!isTest) orbitSpeed -= 2;
    }

    public void setDistance(float dist)
    {
        if (!isTest) orbitDistance = dist;
    }

    public void DistanceOut()
    {
        if (!isTest) orbitDistance += 2;
    }

    public void DistanceIn()
    {
        if (!isTest) orbitDistance -= 2;
    }

    public void activeWater()
    {
        if (waterLayer.activeSelf)
        {
            waterLayer.SetActive(false);
        }
        else
        {
            waterLayer.SetActive(true);
        }

    }

    public void activeMountain()
    {
        if (mountainLayer.activeSelf)
        {
            mountainLayer.SetActive(false);
        }
        else
        {
            mountainLayer.SetActive(true);
        }
    }

    public void setScale(float scale)
    {
        Vector3 newScale = wholePlanet.transform.localScale;
        newScale.x = scale;
        newScale.y = scale;
        newScale.z = scale;
        wholePlanet.transform.localScale = newScale;
        planetScale = wholePlanet.transform.localScale;
    }

    public void scaleUp()
    {
        Vector3 newScale = wholePlanet.transform.localScale;
        newScale.x += 0.1f;
        newScale.y += 0.1f;
        newScale.z += 0.1f;
        wholePlanet.transform.localScale = newScale;
        planetScale = wholePlanet.transform.localScale;
    }

    public void scaleDown()
    {
        Vector3 newScale = wholePlanet.transform.localScale;
        newScale.x -= 0.1f;
        newScale.y -= 0.1f;
        newScale.z -= 0.1f;
        wholePlanet.transform.localScale = newScale;
        planetScale = wholePlanet.transform.localScale;
    }

    public void resetPlanet()
    {
        wholePlanet.transform.localScale = defaultSetting;
        waterLayer.SetActive(defaultWater);
        mountainLayer.SetActive(defaultMountain);
    }
}
