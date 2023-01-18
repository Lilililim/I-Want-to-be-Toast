using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image volumeIcon;
    [SerializeField] Image muteIcon;
    [SerializeField] private AudioSource audioSource;
    private bool mute = false;
    private PlayerMovementNew playerMovement;
    private void Start()
    {
        playerMovement = GameObject.Find("Bread").GetComponent<PlayerMovementNew>();
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        VolumeIcons();
        audioSource.mute = mute;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        playerMovement.canMove = false;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        playerMovement.canMove = true;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void Volume()
    {
        if(mute == false)
        {
            mute = true;
            audioSource.mute = mute;
        }
        else
        {
            mute = false;
            audioSource.mute = mute;
        }
        VolumeIcons();
        Save();
    }
    public void VolumeIcons()
    {
        if (mute == false)
        {
            volumeIcon.enabled = true;
            muteIcon.enabled = false;
        }
        else
        {
            muteIcon.enabled = true;
            volumeIcon.enabled = false;
        }
    }
    private void Load()
    {
        mute = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", mute ? 1 : 0);
    }
}
