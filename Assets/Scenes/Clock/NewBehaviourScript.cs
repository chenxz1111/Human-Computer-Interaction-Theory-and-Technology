using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform secondTransform, minuteTransform, hourTransform;
    public bool discret;
    // Start is called before the first frame update
    private void Awake()
    {
        //Debug.Log("Hello Unity.");
        secondTransform.localRotation = Quaternion.Euler(0, DateTime.Now.Second * 6, 0);
        minuteTransform.localRotation = Quaternion.Euler(0, DateTime.Now.Minute * 6, 0);
        hourTransform.localRotation = Quaternion.Euler(0, DateTime.Now.Hour * 30, 0);
    }

    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (discret)
        {
            //离散写法，时间参数只取整数，可见跳动
            secondTransform.localRotation = Quaternion.Euler(0, DateTime.Now.Second * 6, 0);
            minuteTransform.localRotation = Quaternion.Euler(0, (float)DateTime.Now.TimeOfDay.TotalMinutes * 6, 0);
            hourTransform.localRotation = Quaternion.Euler(0, (float)DateTime.Now.TimeOfDay.TotalHours * 30, 0);
        }
        else
        {
            //连续、圆滑变化
            secondTransform.localRotation = Quaternion.Euler(0, (float)DateTime.Now.TimeOfDay.TotalSeconds * 6, 0);
            minuteTransform.localRotation = Quaternion.Euler(0, (float)DateTime.Now.TimeOfDay.TotalMinutes * 6, 0);
            hourTransform.localRotation = Quaternion.Euler(0, (float)DateTime.Now.TimeOfDay.TotalHours * 30, 0);
        }
    }
    public void Discret()
    {
        discret = !discret;  //点击按钮调用一次，反值
    }
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
        Debug.Log("ss");
    }
}
