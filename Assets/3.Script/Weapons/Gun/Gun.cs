using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Animator animator;

    // ������ IK Ÿ�� ��ġ�� ��Ÿ���� Transform�Դϴ�. 
    // Unity �����Ϳ��� �� ��ġ�� ���� �ڵ� ��ġ�� �°� �����մϴ�.
    public Transform rightHandIKTarget;

    // �޼� IK Ÿ�� ��ġ�� ��Ÿ���� Transform�Դϴ�. 
    // Unity �����Ϳ��� �� ��ġ�� ���� �ѱ� ��ġ�� �°� �����մϴ�.
    public Transform leftHandIKTarget;

    // ��ũ��Ʈ�� ���۵� �� Animator ������Ʈ�� ã���ϴ�.
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // OnAnimatorIK �޼���� Unity�� IK �ý��ۿ��� ȣ��Ǹ�,
    // ���⼭ ĳ������ �� (�޼հ� ������)�� ��ġ�� ������ �����մϴ�.
    void OnAnimatorIK()
    {
        // �������� IK ��ġ�� ������ �����մϴ�.
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIKTarget.rotation);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        // �޼��� IK ��ġ�� ������ �����մϴ�.
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIKTarget.rotation);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 3);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 3);
    }
}
