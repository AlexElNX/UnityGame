using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyFirstProject : MonoBehaviour
{
    private Transform camTransform;
    public GameObject directionLight;
    private Transform lightTransform;
        
    void Start()
    {
        //directionLight = GameObject.Find("Direction Light");

        camTransform = this.GetComponent<Transform>();
        Debug.Log(camTransform.localPosition);

    }
}
