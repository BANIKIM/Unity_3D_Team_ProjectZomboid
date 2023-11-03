using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    
    [SerializeField] private float FadeSpeed;
    [SerializeField] private float FadeAmount;

    MeshRenderer[] renderers;
    Material[] material;
    private GameObject player;
   

    private float originalOpacity = 1f;
    public bool isFade = false;

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        Color color = new Color(0.0f, 0.0f,0.0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            isFade = true;
            
        }
        else
        {
            isFade = false;
        }
    }

    private void FadeNow(MeshRenderer renderer)
    {
        for(int i=0; i<renderer.materials.Length; i++)
        {
            Color currentColor = renderer.material.color;
            Color SmoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, FadeAmount, FadeSpeed * Time.deltaTime));

            renderer.material.color = SmoothColor;
        }
    }

    private void ResetFade(MeshRenderer renderer)
    {
        for(int i=0; i<renderer.materials.Length; i++)
        {
            Color currentColor = renderer.material.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, FadeSpeed * Time.deltaTime));
            renderer.material.color = smoothColor;
        }
    }
 
    


    //Color 
    //Alpha
    


}
