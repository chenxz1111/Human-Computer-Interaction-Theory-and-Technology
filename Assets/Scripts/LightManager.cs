using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Light pointLight;
    public Material onMat;
    public Material offMat;
    public float changeRate = 1.5f;

    float minIntensity = 0.2f;
    float maxIntensity = 10f;

    void Start()
    {
        lightOn();
    }

    public void setLightColor(Color color)
    {
        pointLight.color = color;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void brightnessUp()
    { 
        if(pointLight.intensity < maxIntensity) pointLight.intensity *= changeRate;
    }
    public void brightnessDown()
    {
        if (pointLight.intensity > minIntensity) pointLight.intensity /= changeRate;
    }

    public void setBrightness(float intensity)
    {
        pointLight.intensity = Mathf.Max(minIntensity, Mathf.Min(maxIntensity, intensity));
    }

    public void lightOff()
    {
        foreach (Transform lamp in transform)
        {
            lamp.GetComponent<MeshRenderer>().material = onMat;
        }
        pointLight.gameObject.SetActive(false);
    }

    public void lightOn()
    {
        foreach (Transform lamp in transform)
        {
            lamp.GetComponent<MeshRenderer>().material = onMat;
        }
        pointLight.gameObject.SetActive(true);
        pointLight.intensity = 1;
    }
}
