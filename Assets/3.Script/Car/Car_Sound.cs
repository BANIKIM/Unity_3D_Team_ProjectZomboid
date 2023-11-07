using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Sound : MonoBehaviour
{
    [Header("�õ��Ҹ�")]
    public AudioClip start_up;// �õ��Ҹ�
    [Header("�����Ҹ�")]
    public AudioClip drive;// �����Ҹ�
    [Header("�극��ũ�Ҹ�")]
    public AudioClip brake;// �����Ҹ�

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



    private bool isDriveSoundPlaying = false;

    private IEnumerator Drive_Sound()
    {
        if (isDriveSoundPlaying)
        {
            yield break;
        }

        isDriveSoundPlaying = true;
        audioSource.PlayOneShot(drive);
        yield return new WaitForSeconds(1.3f);

        isDriveSoundPlaying = false;
    }

    public void Start_up()//�õ��Ҹ�
    {
        audioSource.PlayOneShot(start_up);
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
        audioSource.PlayOneShot(drive);
        yield return new WaitForSeconds(1.3f);

        isBrakeSoundPlaying = false;
    }
    public void Brake()//�극��ũ
    {
        StartCoroutine(Brake_Sound());

    }

}
