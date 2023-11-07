using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shot : MonoBehaviour
{
    public Transform tip;//�ѱ�
    public GameObject projectile;
    public AudioClip gunShot;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void ShotEvent()
    {
        Instantiate(projectile, tip.transform.position, tip.transform.rotation);
        audioSource.PlayOneShot(gunShot);

    }

}
