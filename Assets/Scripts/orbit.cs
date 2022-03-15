using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour
{
    private GameObject sun;
    private PlanetProperties properties;
    private Color originalColor;

    private PlanetProperties.PLANET_TYPE planetType = PlanetProperties.PLANET_TYPE.NORMAL;

    private float time;
    private int spawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        sun = GameObject.FindGameObjectWithTag("sun");
        properties = GetComponent<PlanetProperties>();
        originalColor = transform.Find("land").gameObject.GetComponent<Renderer>().material.color;
    }

    private void changeMatColor(Color color)
    {
        Material mat = transform.Find("land").gameObject.GetComponent<Renderer>().material;
        mat.color = color;
        transform.Find("land").gameObject.GetComponent<Renderer>().material = mat;
    }

    private void distanceFromSun()
    {
        if (!properties.isTest)
        {
            if (properties.orbitDistance <= 10 && planetType != PlanetProperties.PLANET_TYPE.HOT)
            {
                changeMatColor(Color.red);
                planetType = PlanetProperties.PLANET_TYPE.HOT;
                properties.ice.SetActive(false);
                properties.fire.SetActive(true);
                Debug.Log("hot");
            }
            if (properties.orbitDistance >= 50 && planetType != PlanetProperties.PLANET_TYPE.FREEZE)
            {
                changeMatColor(Color.blue);
                planetType = PlanetProperties.PLANET_TYPE.FREEZE;
                properties.ice.SetActive(true);
                properties.fire.SetActive(false);
                Debug.Log("Cold");
            }
            else if (properties.orbitDistance > 10 && properties.orbitDistance < 50 && planetType != PlanetProperties.PLANET_TYPE.NORMAL)
            {
                changeMatColor(originalColor);
                planetType = PlanetProperties.PLANET_TYPE.NORMAL;
                properties.ice.SetActive(false);
                properties.fire.SetActive(false);
                Debug.Log("normal");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (time > 1)time = 0;

        time += Time.deltaTime;
        distanceFromSun();

        if (!properties.isTest)
        {
            this.transform.RotateAround(sun.transform.position, Vector3.up, properties.orbitSpeed * Time.deltaTime);
            this.transform.localEulerAngles += new Vector3(1f, 0f, 1f) * properties.rotationSpeed;
            if (properties.orbitDistance != 0)
            {
                if (Vector3.Distance(this.transform.position, sun.transform.position) <= properties.orbitDistance)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, sun.transform.position, -1 * Time.deltaTime);
                }
                else if (Vector3.Distance(this.transform.position, sun.transform.position) >= properties.orbitDistance)
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, sun.transform.position, 1 * Time.deltaTime);
                }
            }
        }
        int seconds = Convert.ToInt32(time % 60);
        if (seconds % 2 == 1)
        {
            switch (planetType)
            {
                case PlanetProperties.PLANET_TYPE.NORMAL:
                    if (spawn == 3) properties.popUp();
                    break;
                case PlanetProperties.PLANET_TYPE.HOT:
                    properties.popDown();
                    break;
                case PlanetProperties.PLANET_TYPE.FREEZE:
                    properties.popDown();
                    break;
            }
            if (spawn == 3) spawn = 0;
            spawn += 1;
            time = 0;
        }



    }
}
