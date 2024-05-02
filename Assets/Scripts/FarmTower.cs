using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
public class FarmTower : MonoBehaviour
{
    [SerializeField]
    private int coolDown = 5;
    [SerializeField]
    private int changeMoney = 10;

    private CameraScript cam;
    private void Start()
    {
        cam = Camera.main.GetComponent<CameraScript>();
        StartCoroutine(AddMoney());
    }

    IEnumerator AddMoney()
    {
        yield return new WaitForSeconds(5);
        cam.ChangeMoney(10);
        StartCoroutine(AddMoney());
    }
}