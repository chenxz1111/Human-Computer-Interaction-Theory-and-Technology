using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    public TextMesh temp_text;
    int temperature = 26;
    int windspeed = 0;
    float time = 0;

    public TextMesh device_text;
    private bool[] deviceConnect = new bool[2];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        temp_text.text = "Temperature:  " +  temperature.ToString() + " ¡æ"
                    + "\n" + "Wind Speed:  " + windspeed.ToString() + " level";

        device_text.text = (deviceConnect[0] ? "Device0 Connect" : "No Device") + '\n' +
                      (deviceConnect[1] ? "Device1 Connect" : "No Device")  ;
        time += Time.deltaTime;
    }

    public void TemperatureUp()
    {
        temperature++;
    }
    public void TemperatureDown()
    {
        temperature--;
    }
    public void TemperatureChange(int delta)
    {
        temperature += delta;
    }
    public void WindSpeedUp()
    {
        if (windspeed < 5)
        {
            windspeed++;
        }
    }
    public void WindSpeedDown()
    {
        if(windspeed > 0)
        {
            windspeed--;
        }
    }

    public void deviceIn(int device_id)
    {
        deviceConnect[device_id] = true;
    }

    public void deviceOut(int device_id)
    {
        deviceConnect[device_id] = false;
    }
}
