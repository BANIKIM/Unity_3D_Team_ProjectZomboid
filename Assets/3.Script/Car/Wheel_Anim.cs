using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_Anim : MonoBehaviour
{
    public WheelCollider frontLeftWheel, frontRightWheel; // �� ���� �� ��
    public WheelCollider rearLeftWheel, rearRightWheel; // �� ���� �� ��
    private float Rot = 0f;

    private void FixedUpdate()
    {
        
    }


    private void Wheel_spin()//���� ������ ������
    {
        Rot += 0.1f;
        frontLeftWheel.transform.Rotate(Rot, 0, 0);
        frontRightWheel.transform.Rotate(Rot, 0, 0);
        rearLeftWheel.transform.Rotate(Rot, 0, 0);
        rearRightWheel.transform.Rotate(Rot, 0, 0);
    }
}
