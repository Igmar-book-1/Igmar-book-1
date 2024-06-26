using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

//TP2 - Matias Sueldo
public class SoundManagerController : MonoBehaviour
{

    public static SoundManagerController instance { get; private set; }
    public bool AllSoundsActive { get; set; }
    public float AllVolume { get; set; }
    public bool MusicActive { get; set; }
    public float MusicVolume { get; set; }
    public bool FxActive { get; set; }
    public float FxVolume { get; set; }
    public bool AmbientActive { get; set; }
    public float AmbientVolume { get; set; }
    [SerializeField]protected Slider generalVolumeSlider;
    [SerializeField] protected Slider musicVolumeSlider;
    [SerializeField] protected Slider fxVolumeSlider;
    [SerializeField] protected Slider ambientVolumeSlider;

    public AudioSource[] music;
    public AudioSource[] fx;
    public AudioSource[] ambient;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


    }
    private void Start()
    {
        //generalVolumeSlider = GameManager.instance.GeneralSoundSlider;
        //ambientVolumeSlider = GameManager.instance.AmbientSlider;
        //fxVolumeSlider = GameManager.instance.FxSlider;
        //musicVolumeSlider = GameManager.instance.MusicSlider;

        generalVolumeSlider.onValueChanged.AddListener((v) => VolumeAllSounds(v));
        ambientVolumeSlider.onValueChanged.AddListener((v) => VolumeAmbient(v));
        fxVolumeSlider.onValueChanged.AddListener((v) => VolumeFx(v));
        musicVolumeSlider.onValueChanged.AddListener((v) => VolumeMusic(v));
        AllVolume = 1;
        MusicVolume = 1;
        AmbientVolume = 1;
        FxVolume = 1;
    }
    void Update()
    {


    }
    public void ToggleAllSounds()
    {
        if(AllSoundsActive)
        {
            if (MusicActive) ToggleMusic();
            if (FxActive) ToggleFx() ;
            if (AmbientActive) ToggleAmbient() ;
            AllSoundsActive = false;
        }
        else
        {
            if (!MusicActive) ToggleMusic();
            if (!FxActive) ToggleFx();
            if (!AmbientActive) ToggleAmbient();
            AllSoundsActive = true;
        }
    }

    public void ToggleMusic()
    {
        silenceArray(music);
        MusicActive = !MusicActive;
    }

    public void ToggleFx()
    {
        silenceArray(fx);
        FxActive = !FxActive;
    }

    public void ToggleAmbient()
    {
        silenceArray(ambient);
        AmbientActive = !AmbientActive;
    }

    public void VolumeAllSounds(float volume)
    {
        AllVolume = volume;
        VolumeMusic(MusicVolume);
        VolumeFx(FxVolume);
        VolumeAmbient(AmbientVolume);
    }

    public void VolumeMusic(float volume)
    {
        MusicVolume = volume;
        adjustVolume(volume, music);

    }

    public void VolumeFx(float volume)
    {
        FxVolume = volume;
        adjustVolume(volume, fx);
    }

    public void VolumeAmbient(float volume)
    {
        AmbientVolume = volume;
        adjustVolume(volume, ambient);
    }
    
    public void adjustVolume(float localVolume, AudioSource[] audioSources)
    {
        float soundCalc = localVolume * AllVolume / 1;
        foreach (AudioSource source in audioSources)
        {
            source.volume = soundCalc;
        }
    }


    public void silenceArray(AudioSource[] audiosources)
    {
        foreach (AudioSource source in audiosources)
        {
            source.mute = !source.mute;
        }
    }

}
