using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("휠콜라이더")]
    public WheelCollider frontLeftWheel, frontRightWheel; // 앞 바퀴 두 개
    public WheelCollider rearLeftWheel, rearRightWheel; // 뒷 바퀴 두 개
    public Car_Sound car_Sound;



    [Header("애니메이션")]
    public GameObject frontLeftWheel_Ain, frontRightWheel_Ain; // 앞 바퀴 두 개
    public GameObject rearLeftWheel_Ain, rearRightWheel_Ain; // 뒷 바퀴 두 개

    [Header("라이트")]
    [SerializeField] private GameObject Light_R;
    [SerializeField] private GameObject Light_L;
    private bool Light = false;

    public float motorForce = 1000; // 모터 힘
    public float steeringAngle = 40f; // 스티어링 각도
    private bool isStart_up = false;
    private float Rot = 0f;
    private float Rot_Y = 0f;
    private void Start()
    {
        car_Sound = GetComponent<Car_Sound>();
    }

    private void FixedUpdate()
    {
        var motorInput = Input.GetAxis("Vertical") * motorForce; // 수직 입력(키보드의 W와 S 또는 위쪽 화살표와 아래쪽 화살표 키)
        var steeringInput = Input.GetAxis("Horizontal") * steeringAngle; // 수평 입력(키보드의 A와 D 또는 왼쪽 화살표와 오른쪽 화살표 키)

        if (Input.GetKeyDown(KeyCode.F))//라이트 켜고 끄기
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
        // 모터 토크 적용
        frontLeftWheel.motorTorque = motorInput;
        frontRightWheel.motorTorque = motorInput;
        rearLeftWheel.motorTorque = motorInput;
        rearRightWheel.motorTorque = motorInput;

        // 스티어링 각도 적용
        frontLeftWheel.steerAngle = steeringInput;
        frontRightWheel.steerAngle = steeringInput;

    }


    private void Wheel_spin(float steeringInput)//바퀴 앞으로 굴리기
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