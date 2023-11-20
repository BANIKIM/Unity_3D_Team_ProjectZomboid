using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Sound : MonoBehaviour
{
    /*[Header("�õ��Ҹ�")]
    public AudioClip start_up;// �õ��Ҹ�
    [Header("�����Ҹ�")]
    public AudioClip drive;// �����Ҹ�
    [Header("�극��ũ�Ҹ�")]
    public AudioClip brake;// �����Ҹ�

    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
*/
    private bool isDriveSoundPlaying = false;

    private IEnumerator Drive_Sound()
    {
        if (isDriveSoundPlaying)
        {
            yield break;
        }

        isDriveSoundPlaying = true;
        // audioSource.PlayOneShot(drive);
        MusicController.instance.PlaySFXSound("Car_Dirve");
        yield return new WaitForSeconds(0.9f);

        isDriveSoundPlaying = false;
    }

    public void Start_up()//�õ��Ҹ�
    {
        // audioSource.PlayOneShot(start_up);
        MusicController.instance.PlaySFXSound("Car_StartUp");
    }

    public void Drive()//�����Ҹ�
    {
        StartCoroutine(Drive_Sound());
    }


    private bool isBrakeSoundPlaying = false;

    private IEnumerator Brake_Sound()
    {
        if (isBrakeSoundPlaying)
        {
            yield break;
        }

        isBrakeSoundPlaying = true;
        // audioSource.PlayOneShot(brake);
        MusicController.instance.PlaySFXSound("Car_Brake");
        yield return new WaitForSeconds(1.5f);

        isBrakeSoundPlaying = false;
    }
    public void Brake()//�극��ũ
    {
        StartCoroutine(Brake_Sound());
    }

}
