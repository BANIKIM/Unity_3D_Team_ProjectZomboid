using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInteract : MonoBehaviour
{
    public GameObject Player;
    public Player_isCar player_isCar;
    public GameObject Vehicle;
    public GameObject Carmer;
    private bool inVehicle = false;


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && !inVehicle && player_isCar.iscar)
        {
            EnterVehicle(); // ������ ž��
        }
        else if (Input.GetKeyDown(KeyCode.E) && inVehicle)
        {
            ExitVehicle(); // �������� ����
        }
    }



    void EnterVehicle()
    {
        // �÷��̾ ���� ���ο� ��ġ
        Player.transform.position = Vehicle.transform.position;
        Player.SetActive(false);

        // ���� ���� Ȱ��ȭ
        Vehicle.GetComponent<VehicleController>().enabled = true;
        Carmer.GetComponent<Camera_Controller>().enabled = false;
        Carmer.GetComponent<Camera_Controller_Car>().enabled = true;


        inVehicle = true;
    }

    void ExitVehicle()
    {
        // �÷��̾ ���� �ܺο� ��ġ
        Player.transform.position = Vehicle.transform.position + new Vector3(2, 0, 0);
        Player.SetActive(true);

        // ���� ���� ��Ȱ��ȭ
        Vehicle.GetComponent<VehicleController>().enabled = false;
        Carmer.GetComponent<Camera_Controller>().enabled = true;
        Carmer.GetComponent<Camera_Controller_Car>().enabled = false;

        inVehicle = false;
    }
}