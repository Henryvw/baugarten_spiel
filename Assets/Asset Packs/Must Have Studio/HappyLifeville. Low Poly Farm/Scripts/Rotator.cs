using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 _value;
    public bool Enable_Noise;
    public Vector3 _Noisevalue;
    public float NoiseUpdateRate =3;
    Vector3 smoothFactor;
    Vector3 smoothFactor2;
    float RefreshTime =10;
    void Update()
    {
        RefreshTime+= Time.deltaTime;
        if(Enable_Noise&&RefreshTime>NoiseUpdateRate)
        {
            RefreshTime=0;
            smoothFactor2 = new Vector3(Random.Range(-_Noisevalue.x,_Noisevalue.x),Random.Range(-_Noisevalue.y,_Noisevalue.y),Random.Range(-_Noisevalue.z,_Noisevalue.z));
        }
        smoothFactor = Vector3.Lerp(smoothFactor,smoothFactor2,Time.deltaTime*2);
        Vector3 localValue = (_value+smoothFactor)*Time.deltaTime*5;
        transform.eulerAngles = transform.eulerAngles+localValue;
    }
}
