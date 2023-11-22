using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using DigitalRuby.RainMaker;

#region Enum
public enum SliderList
{
    SFX = 0,
    BGM,
}

public enum BGMSound
{
    Main = 0,
    Load,
    Game,
}

public enum SFXSound
{
    Player_FootStep = 0,
    Player_Run,
    Player_Die,
    Player_BatSwing,
    Player_Hit,
    Zombie_Hit,
    Zombie_Die,
    Car_StartUp,
    Car_Dirve,
    Car_Brake,
    Car_InOut,
    Window_Bottele,
    Door_Open, // door open, broken, crash ã��
    Door_Broken,
    Door_Crash,
    Gun_Shot,
    Rock_Hit,
    Rock_Broken,
}
#endregion
public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    public AudioMixer audioMixer;
    private Canvas canvas;

    // RainSound
    private RainScript rainScript = null;

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    [SerializeField] private GameObject musicUI;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private GameObject[] sliderObject;
    [SerializeField] private Slider[] slider;

    private Button settingButton;
    private GameObject musicSettingPanel;

    public float bgmVolume = 0f;
    public float sfxVolume = 0f;

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
        TryGetComponent(out bgmPlayer);
        TryGetComponent(out sfxPlayer);
        rainScript = FindObjectOfType<RainScript>();

        AwakeSetting();
        SettingButton();
        PlayerPrefs.SetFloat("BGMVolume", 0f);
        PlayerPrefs.SetFloat("SFXVolume", 0f);
    }

    public void ChangeSceneMusic(string type)
    {
        // Scene�� �ٲ� ��µǴ� �޼ҵ�
        AwakeSetting();
        OnEnableMusic();
        PlayBGMSound(type);
    }

    public void AwakeSetting()
    {
        musicSettingPanel = null;
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").transform.GetComponent<Canvas>();
        }
    }

    public void SettingButton()
    {
        settingButton = null;
        if (settingButton == null)
        {
            settingButton = GameObject.FindGameObjectWithTag("SettingMenu").transform.GetChild(2).gameObject.transform.GetComponent<Button>();
            settingButton.onClick.AddListener(SetActiveTrue);
        }
    }

    public void OnEnableMusic()
    {
        // Sound PlayerPrefs Check
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            bgmVolume = PlayerPrefs.GetFloat("BGMVolume");
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void OnDisableMusic()
    {
        settingButton.onClick.RemoveAllListeners(); // Event Remove All
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("TestIntro_UIFix")) // IntroScene name
        {
            PlayBGMSound("Main"); // GameStart
        }
    }

    private void SetActiveTrue()
    {
        // Setting Button Click, Menu activeSelf true
        if (musicSettingPanel == null)
        {
            musicSettingPanel = Instantiate(musicUI, canvas.transform); // ������� canvas ���������� ������, canvas ���ľ���
            sliderObject = GameObject.FindGameObjectsWithTag("MusicSlider");
            if (musicSettingPanel.activeSelf)
            {
                for (int i = 0; i < sliderObject.Length; i++)
                {
                    slider[i] = sliderObject[i].transform.GetComponent<Slider>();
                }
                slider[(int)SliderList.BGM].onValueChanged.AddListener(SetBGMVolume);
                slider[(int)SliderList.SFX].onValueChanged.AddListener(SetSFXVolume);
                SliderHandlerPosition();
            }
        }
        if (!musicSettingPanel.activeSelf)
        {
            musicSettingPanel.SetActive(true);
        }
    }

    public void SliderHandlerPosition()
    {
        // ��ư Ŭ�� �� �����̴� �ڵ鷯 ��ġ
        SetBGMVolume(PlayerPrefs.GetFloat("BGMVolume"));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        slider[(int)SliderList.BGM].value = PlayerPrefs.GetFloat("BGMVolume");
        slider[(int)SliderList.SFX].value = PlayerPrefs.GetFloat("SFXVolume");
    }

    #region Sound Play
    private void PlayBGMSound(string type)
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
        sfxPlayer.PlayOneShot(sfxPlayer.clip);
    }
    #endregion
    #region Volume Setting
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
        bgmVolume = volume;
        AudioListenerVolume("BGM", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        sfxVolume = volume;
        AudioListenerVolume("SFX", volume);
    }

    private void AudioListenerVolume(string type, float volume)
    {
        // AudioSource �Ҵ�, volume�� ���� ���Ұ� ����
        AudioSource typeAudio = null;
        if (type.Equals("BGM"))
        {
            typeAudio = bgmPlayer;
        }
        else if (type.Equals("SFX"))
        {
            typeAudio = sfxPlayer;
        }
        // Audio mute
        if (volume.Equals(-80f))
        {
            typeAudio.mute = true;
        }
        else
        {
            typeAudio.mute = false;
        }
    }
    #endregion
}
