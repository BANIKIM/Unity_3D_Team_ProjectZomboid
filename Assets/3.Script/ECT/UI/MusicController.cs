using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SliderList
{
    ALL = 0,
    BGM,
    SFX,
}

public enum BGMSound
{
    Main = 0,
    Game,
}

public enum SFXSound
{
    PlayerWalk = 0,
    Zombie,
}

public class MusicController : MonoBehaviour
{
    public MusicController instance;

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips; // BGM �̿��� Audio Clip ���� ��������
    [SerializeField] private Slider[] slider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else
        {
            Destroy(instance);
        }
        bgmPlayer = FindObjectOfType<AudioSource>();
        sfxPlayer = FindObjectOfType<AudioSource>();

        bgmPlayer.volume = 100;
        sfxPlayer.volume = 100;

        PlayBGMSound("Main");
    }

    #region Sound Play
    public void PlayBGMSound(string type)
    {
        // ����� �÷���
        if (bgmPlayer.isPlaying)
        {
            bgmPlayer.Stop();
        }
        int index = (int)(BGMSound)Enum.Parse(typeof(BGMSound), type); // string�� enum���� ���� �� int�� ����
        bgmPlayer.clip = bgmClips[index];
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }
    public void PlaySFXSound(string type)
    {
        // ȿ���� �÷���
        int index = (int)(SFXSound)Enum.Parse(typeof(BGMSound), type);
        sfxPlayer.clip = sfxClips[index];
        sfxPlayer.Play();
    }
    #endregion
    #region Volume Setting
    public void AllSetting()
    {
        slider[(int)SliderList.ALL].onValueChanged.AddListener(ChangeBGMVolume);
        slider[(int)SliderList.ALL].onValueChanged.AddListener(ChangeSFXVolume);
    }

    public void BGMSetting()
    {
        slider[(int)SliderList.BGM].onValueChanged.AddListener(ChangeBGMVolume);
    }

    public void SFXSetting()
    {
        slider[(int)SliderList.BGM].onValueChanged.AddListener(ChangeSFXVolume);
    }

    // Slider volume setting
    private void ChangeBGMVolume(float value)
    {
        bgmPlayer.volume = value;
    }

    private void ChangeSFXVolume(float value)
    {
        sfxPlayer.volume = value;
    }
    #endregion
}
