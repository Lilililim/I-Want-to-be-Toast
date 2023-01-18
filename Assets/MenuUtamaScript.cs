using UnityEngine;
using UnityEngine.UI;

public class MenuUtamaScript : MonoBehaviour
{
    [SerializeField] Image volumeIcon;
    [SerializeField] Image muteIcon;
    [SerializeField] private AudioSource audioSource;
    private bool mute = false;
    private void Start()
    {
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
    public void Change()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void Volume()
    {
        if (mute == false)
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
