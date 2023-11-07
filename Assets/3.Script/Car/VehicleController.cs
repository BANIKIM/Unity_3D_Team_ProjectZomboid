using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public WheelCollider frontLeftWheel, frontRightWheel; // �� ���� �� ��
    public WheelCollider rearLeftWheel, rearRightWheel; // �� ���� �� ��
    public Car_Sound car_Sound;

    public float motorForce = 50f; // ���� ��
    public float steeringAngle = 30f; // ��Ƽ� ����
    private bool isStart_up=false;

    private void Start()
    {
        car_Sound = GetComponent<Car_Sound>();
    }

    private void FixedUpdate()
    {
        var motorInput = Input.GetAxis("Vertical") * motorForce; // ���� �Է�(Ű������ W�� S �Ǵ� ���� ȭ��ǥ�� �Ʒ��� ȭ��ǥ Ű)
        var steeringInput = Input.GetAxis("Horizontal") * steeringAngle; // ���� �Է�(Ű������ A�� D �Ǵ� ���� ȭ��ǥ�� ������ ȭ��ǥ Ű)

        if (!isStart_up)
        {
            car_Sound.Start_up();
            isStart_up = true;
        }
        else if(motorInput!=0&&isStart_up)
        {
            car_Sound.Drive();
            ApplyInput(motorInput, steeringInput);            
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
}