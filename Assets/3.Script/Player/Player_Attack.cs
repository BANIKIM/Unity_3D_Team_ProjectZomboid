using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator anim;
    public bool isAttack;
    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            anim.SetLayerWeight(1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("isSwing");
                isAttack = true;
            }
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }
}
