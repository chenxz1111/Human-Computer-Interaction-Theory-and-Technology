using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    public TextMesh temp_text;
    int temperature = 26;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        temp_text.text = temperature.ToString() + " ¡æ";
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
}
