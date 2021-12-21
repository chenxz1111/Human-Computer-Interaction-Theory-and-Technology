using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomMode : MonoBehaviour
{
    // Start is called before the first frame update
    bool isWork = true;
    public static int mode;
    public Color relaxColor;
    void Start()
    {
        
    }
    public void toWorkMode()
    {
        if (!isWork)
        {
            isWork = true;
            LightManager.pointLight.color = Color.white;
            TextManager.temperature = 25;
            TextManager.windspeed = 3;
        }
    }
    public void toRelaxMode()
    {
        if (isWork)
        {
            isWork = false;
            LightManager.pointLight.color = relaxColor;
            TextManager.temperature = 27;
            TextManager.windspeed = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
