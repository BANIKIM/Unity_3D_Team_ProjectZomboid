using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_skid : MonoBehaviour
{
    public VehicleController vehicleController;


    public Transform hitPoint1;//����Ʈ
    public Transform hitPoint2;//����Ʈ
    public GameObject projectile;


    private void Update()
    {
        if(vehicleController.aaa)
        {
            
            AttackEvent();
        }
    }

    public void AttackEvent()
    {
        Instantiate(projectile, hitPoint1.transform.position, hitPoint1.transform.rotation);
        Instantiate(projectile, hitPoint2.transform.position, hitPoint2.transform.rotation);
    }
}
