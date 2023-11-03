using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Transparent : MonoBehaviour
{
    private TransparentObject fader;

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("House");

        if(player != null)
        {
            Vector3 direct = player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direct.normalized);
            RaycastHit[] hit;

            hit = Physics.RaycastAll(transform.position, transform.position, 1f);

          for(int i = 0;i<hit.Length; i++)
            {
                if (Physics.Raycast(ray, out hit[i]))
                {

                    if (hit[i].collider == null)
                        return;

                    fader = hit[i].collider.gameObject.GetComponent<TransparentObject>();

                    if (hit[i].collider.gameObject == player)
                    {

                        if (fader != null)
                        {
                            //fader.isDoFade = false;
                            Debug.Log("House");
                        }
                    }
                    else
                    {
                        if (fader == null)
                        {
                            //fader.isDoFade = true;
                        }
                    }

                }
            }
        }
    }

}
