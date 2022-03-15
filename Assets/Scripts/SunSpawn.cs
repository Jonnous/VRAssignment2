using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawn : MonoBehaviour
{
    public GameObject[] planets = new GameObject[5];
    public int planetCount = 0;

    public void spawnPlanet(PlanetProperties properties)
    {
        if (planetCount <= 4)
        {
            planets[planetCount] = Instantiate(properties.planetPrefab);
            planets[planetCount].transform.GetComponent<PlanetProperties>().isTest = false;
            planets[planetCount].transform.position = this.transform.position + new Vector3(0, 0, planets[planetCount].transform.GetComponent<PlanetProperties>().orbitDistance);
            planets[planetCount].transform.GetComponent<PlanetProperties>().preset(properties.getPlanetScale(), properties.getWaterLayerActive(), properties.getMountainLayerActive());
            planets[planetCount].transform.GetComponent<PlanetProperties>().setOrbitSpeed(20);
            planets[planetCount].transform.GetComponent<PlanetProperties>().setRotationSpeed(1);
            planets[planetCount].transform.GetComponent<PlanetProperties>().setPopulation(10);
            planetCount += 1;
        }
        else { Debug.Log("Planet list full"); }


    }
}
