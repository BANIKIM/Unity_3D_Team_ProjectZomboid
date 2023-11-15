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
    public float steeringAngle = 45f; // ��Ƽ� ����
    private bool isStart_up = false;
    private float Rot = 0f;
    /* private float defaultStiffness;  // �⺻ Ÿ�̾� �������� ������ ����*/
    private float Oil=24f; //�⸧


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
        else if (motorInput != 0 && isStart_up && Oil>0)
        {
            car_Sound.Drive();
            ApplyInput(motorInput, steeringInput);
            //Wheel_spin(steeringInput);
            Oil -= 0.1f*Time.deltaTime;//�⸧�ٴ°�
            Debug.Log("�⸧������ : "+Oil);
        }

        //�극��ũ
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // ����Ʈ Ű�� ������ ���, �극��ũ�� ���۽�ŵ�ϴ�.
            ApplyBrake();
            car_Sound.Brake();
        }
        else
        {
            // ����Ʈ Ű�� �������� ���� ���, �극��ũ ��ũ�� 0���� �����Ͽ� �극��ũ�� �����մϴ�.
            frontLeftWheel.brakeTorque = 0;
            frontRightWheel.brakeTorque = 0;
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
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
        float a = frontLeftWheel.steerAngle = steeringInput;
        float b = frontRightWheel.steerAngle = steeringInput;

        Rot += 5f;
        Quaternion rotation_L = Quaternion.Euler(Rot, a - 80f, 0);
        Quaternion rotation_R = Quaternion.Euler(Rot, b + 80f, 0);
        

        frontLeftWheel_Ain.transform.rotation = rotation_L;
        frontRightWheel_Ain.transform.rotation = rotation_R;
        rearLeftWheel_Ain.transform.Rotate(Rot,0,0);
        rearRightWheel_Ain.transform.Rotate(Rot, 0, 0);
    }

    private void ApplyBrake()
    {
        float brakeForce = motorForce;  // �극��ũ ���� �Ϲ������� ���� ���� �����ϰ� �����մϴ�.
        frontLeftWheel.brakeTorque = brakeForce;
        frontRightWheel.brakeTorque = brakeForce;
        rearLeftWheel.brakeTorque = brakeForce;
        rearRightWheel.brakeTorque = brakeForce;
    }







}