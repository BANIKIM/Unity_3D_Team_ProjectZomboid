using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPanelController : MonoBehaviour
{
    public void BackButton()
    {
        gameObject.SetActive(false);
        MusicController.instance.SliderMusicSetting();
    }

    public void OKButton()
    {
        // Audio Volume ���� �Ϸ�
        PlayerPrefs.SetFloat("BGMVolume", MusicController.instance.bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", MusicController.instance.sfxVolume);
    }
}
