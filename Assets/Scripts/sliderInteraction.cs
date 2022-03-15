using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class sliderInteraction : MonoBehaviour
{
    public GameObject handleRef;
    public SteamVR_Action_Boolean triggerPress;
    public float sliderValue = 0;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("handle trigger");
        if (other.gameObject.Equals(handleRef))
        {
            moveSlider();
        }
    }

    public void moveSlider()
    {
        if (triggerPress.state)
        {
            Debug.Log("move slider");
            handleRef.transform.position = this.transform.position;
            float xPos = handleRef.transform.localPosition.x;
            float clampedXPos = Mathf.Clamp(xPos, 0f, 1f);
            handleRef.transform.localPosition = new Vector3(clampedXPos, 0f, 0f);
            sliderValue = Mathf.Abs(clampedXPos) *2;
            if (transform.gameObject.GetComponent<RayCast>().properties != null) transform.gameObject.GetComponent<RayCast>().properties.setScale(sliderValue); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
