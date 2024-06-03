using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
public class FarmTower : MonoBehaviour
{
    [SerializeField]
    private float coolDown = 5;
    [SerializeField]
    private int changeMoney = 25;

    private CameraScript cam;
    private void Start()
    {
        cam = Camera.main.GetComponent<CameraScript>();
        StartCoroutine(AddMoney());
    }

    IEnumerator AddMoney()
    {
        yield return new WaitForSeconds(coolDown);
        cam.ChangeMoney(changeMoney);
        StartCoroutine(AddMoney());
    }
}