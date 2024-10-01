using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private VibrationManager vibrationManager;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;
    [SerializeField] private Sprite optionOnSprite;
    [SerializeField] private Sprite optionOffSprite;

    [Header("Setting")]
    private bool soundsState = true;
    private bool hapticsState = true;

    private void Awake()
    {
        soundsState = PlayerPrefs.GetInt("sounds",1) == 1;
        hapticsState = PlayerPrefs.GetInt("haptics", 1) == 1;
    }

    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Setup()
    {
        if (soundsState)

            EnableSounds();


        else
            DisableSounds();


        if (hapticsState)

            EnableHaptics();
        else
            DisableHaptics();
    }


    public void ChangeSoundsState()
    {
        if (soundsState)
        
            DisableSounds();
        

        else 
            EnableSounds();
        

        soundsState = !soundsState;

       /* int soundSaveState = 0;

        if (soundsState)

            soundSaveState = 1;

        else
            soundSaveState = 0 */

        PlayerPrefs.SetInt("sounds", soundsState? 1 : 0);

    }

    private void DisableSounds()
    {
        // Tell the sound manager to set the volume of all sounds to 0
        soundManager.DisableSounds();

        //Chanage the image of the sounds button 
        soundsButtonImage.sprite = optionOffSprite;
    }
    
    private void EnableSounds()
    {
        soundManager.EnableSounds();

        //Chanage the image of the sounds button 
        soundsButtonImage.sprite = optionOnSprite;
    }


    public void ChangeHapticsState()
    {
        if (hapticsState)

            DisableHaptics();


        else
            EnableHaptics();


        hapticsState = !hapticsState;

        PlayerPrefs.SetInt("haptics", hapticsState ? 1 : 0);

    }

    private void DisableHaptics()
    {
        vibrationManager.DisableVibration();

        hapticsButtonImage.sprite = optionOffSprite;

    }

    private void EnableHaptics()
    {
        vibrationManager.EnableVibration();

        hapticsButtonImage.sprite = optionOnSprite;
    }
}
