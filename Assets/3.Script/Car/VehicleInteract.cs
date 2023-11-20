using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInteract : MonoBehaviour
{
    public GameObject Player;
    public Player_isCar player_isCar;
    public GameObject Vehicle;
    public GameObject Carmer;
    public Rigidbody rig;
    private bool inVehicle = false;
    /*[Header("������")]
    public AudioClip Car_in;
    public AudioClip Car_out;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inVehicle && player_isCar.iscar)
        {

            StartCoroutine(EnterVehicle()); // ������ ž��
            
        }
        else if (Input.GetKeyDown(KeyCode.E) && inVehicle)
        {
            StartCoroutine(ExitVehicle()); // �������� ����
        }
    }

   private IEnumerator EnterVehicle()
    {
        // audioSource.PlayOneShot(Car_in);
        MusicController.instance.PlaySFXSound("Car_InOut");
        yield return new WaitForSeconds(1f);
        // �÷��̾ ���� ���ο� ��ġ
        Player.transform.position = Vehicle.transform.position;
        Player.SetActive(false);

        // ���� ���� Ȱ��ȭ
        Vehicle.GetComponent<VehicleController>().enabled = true;
        Carmer.GetComponent<Camera_Controller>().enabled = false;
        Carmer.GetComponent<Camera_Controller_Car>().enabled = true;

        rig.isKinematic = false;//Ű�׸�ƽ�� ��Ȱ��ȭ ���� �����̰� ��
        inVehicle = true;
    }

    private IEnumerator ExitVehicle()
    {
        // audioSource.PlayOneShot(Car_out);
        MusicController.instance.PlaySFXSound("Car_InOut");
        yield return new WaitForSeconds(1f);

        // �÷��̾ ���� �ܺο� ��ġ
        Player.transform.position = Vehicle.transform.position + new Vector3(2, 0, 0);
        Player.SetActive(true);

        // ���� ���� ��Ȱ��ȭ
        Vehicle.GetComponent<VehicleController>().enabled = false;
        Carmer.GetComponent<Camera_Controller>().enabled = true;
        Carmer.GetComponent<Camera_Controller_Car>().enabled = false;
        rig.isKinematic = true;//Ű�׸�ƽ�� Ȱ��ȭ ���� �����̰� ��
        inVehicle = false;
    }
}