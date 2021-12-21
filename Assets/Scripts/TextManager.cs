using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    public TextMesh temp_text;
    static public int temperature = 26;
    static public int windspeed = 0;
    float time = 0;
    public RectTransform[] device = new RectTransform[2];
    public RectTransform[] deviceEmpty = new RectTransform[2];

    public TextMesh device_text;
    private bool[] deviceConnect = new bool[2];
    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i < 2; i++)
        {
            device[i].gameObject.SetActive(false);
            deviceEmpty[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        temp_text.text = "Temperature:  " +  temperature.ToString() + " ¡æ"
                    + "\n" + "Wind Speed:  " + windspeed.ToString() + " level";

        device_text.text = (deviceConnect[0] ? "Device0 Connect: power: 93%" : "No Device") + '\n' +
                      (deviceConnect[1] ? "Device1 Connect: power: 57% " : "No Device")  ;
        time += Time.deltaTime;
    }

    public void TemperatureUp()
    {
        if(temperature < 30) temperature++;
    }
    public void TemperatureDown()
    {
        if (temperature > 16) temperature--;
    }
    public void TemperatureChange(int delta)
    {
        temperature += delta;
    }

    public void SetTemperature(int t)
    {
        temperature = t;
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
        device[device_id].gameObject.SetActive(true);
        deviceEmpty[device_id].gameObject.SetActive(false);
    }

    public void deviceOut(int device_id)
    {
        deviceConnect[device_id] = false;
        device[device_id].gameObject.SetActive(false);
        deviceEmpty[device_id].gameObject.SetActive(true);
    }
}
