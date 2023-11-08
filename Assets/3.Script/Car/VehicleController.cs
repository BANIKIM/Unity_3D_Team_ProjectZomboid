using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("���ݶ��̴�")]
    public WheelCollider frontLeftWheel, frontRightWheel; // �� ���� �� ��
    public WheelCollider rearLeftWheel, rearRightWheel; // �� ���� �� ��
    public Car_Sound car_Sound;



    [Header("�ִϸ��̼�")]
    public GameObject frontLeftWheel_Ain, frontRightWheel_Ain; // �� ���� �� ��
    public GameObject rearLeftWheel_Ain, rearRightWheel_Ain; // �� ���� �� ��

    [Header("����Ʈ")]
    [SerializeField] private GameObject Light_R;
    [SerializeField] private GameObject Light_L;
    private bool Light = false;

    public float motorForce = 1000; // ���� ��
    public float steeringAngle = 40f; // ��Ƽ� ����
    private bool isStart_up = false;
    private float Rot = 0f;
    private float Rot_Y = 0f;
    private void Start()
    {
        car_Sound = GetComponent<Car_Sound>();
    }

    private void FixedUpdate()
    {
        var motorInput = Input.GetAxis("Vertical") * motorForce; // ���� �Է�(Ű������ W�� S �Ǵ� ���� ȭ��ǥ�� �Ʒ��� ȭ��ǥ Ű)
        var steeringInput = Input.GetAxis("Horizontal") * steeringAngle; // ���� �Է�(Ű������ A�� D �Ǵ� ���� ȭ��ǥ�� ������ ȭ��ǥ Ű)

        if (Input.GetKeyDown(KeyCode.F))//����Ʈ �Ѱ� ����
        {
            if (!Light)
            {
                Light_R.SetActive(true);
                Light_L.SetActive(true);
                Light = true;
            }
            else
            {
                Light_R.SetActive(false);
                Light_L.SetActive(false);
                Light = false;
            }
        }

        if (!isStart_up)
        {
            car_Sound.Start_up();
            isStart_up = true;
        }
        else if (motorInput != 0 && isStart_up)
        {
            car_Sound.Drive();
            ApplyInput(motorInput, steeringInput);
            Wheel_spin(steeringInput);
            
        }

    }

    private void ApplyInput(float motorInput, float steeringInput)
    {
        // ���� ��ũ ����
        frontLeftWheel.motorTorque = motorInput;
        frontRightWheel.motorTorque = motorInput;
        rearLeftWheel.motorTorque = motorInput;
        rearRightWheel.motorTorque = motorInput;

        // ��Ƽ� ���� ����
        frontLeftWheel.steerAngle = steeringInput;
        frontRightWheel.steerAngle = steeringInput;

    }


    private void Wheel_spin(float steeringInput)//���� ������ ������
    {
        Rot += 0.1f;

        Quaternion rotation = Quaternion.Euler(Rot, Rot_Y, 0);
        frontLeftWheel_Ain.transform.rotation = rotation;
        frontRightWheel_Ain.transform.rotation = rotation;

        rotation = Quaternion.Euler(Rot, 0, 0);
        rearLeftWheel_Ain.transform.rotation = rotation;
        rearRightWheel_Ain.transform.rotation = rotation;

        if (steeringInput > 0)
        {
            if (Rot_Y < 30)
            {
                Rot_Y += 0.05f;
            }

            rotation = Quaternion.Euler(Rot, Rot_Y, 0);
            frontLeftWheel_Ain.transform.rotation = rotation;
            frontRightWheel_Ain.transform.rotation = rotation;
        }
        else if (steeringInput < 0)
        {
            if (Rot_Y > -30)
            {
                Rot_Y -= 0.05f;
            }

            rotation = Quaternion.Euler(Rot, Rot_Y, 0);
            frontLeftWheel_Ain.transform.rotation = rotation;
            frontRightWheel_Ain.transform.rotation = rotation;
        }
    }

}