using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region Resources
    [HideInInspector] public /*static*/ AudioSource sfxSource;
    public static AudioClip duckSFX, jumpSFX, checkpointSFX, butterSFX, winSFX;
    #endregion

    #region Components
    private PlayerMovementNew playerMovement;
    private PlayerCollisions playerCollision;
    private AudioSource musicControlAudioSource;
    #endregion

    private void Start()
    {
        SetupAudioclip();
        playerMovement = GetComponent<PlayerMovementNew>();
        playerCollision = GetComponent<PlayerCollisions>();
        musicControlAudioSource =  GameObject.Find("MusicController").GetComponent<AudioSource>();
        Debug.Log(musicControlAudioSource);
    }
    private void SetupAudioclip()
    {
        sfxSource = GetComponent<AudioSource>();
        duckSFX = Resources.Load<AudioClip>("duck_quack");
        jumpSFX = Resources.Load<AudioClip>("jump_02");
        checkpointSFX = Resources.Load<AudioClip>("checkpointSFX");
        butterSFX = Resources.Load<AudioClip>("Pickup_04");
        winSFX = Resources.Load<AudioClip>("Win_SFX");
    }

    public void PlayAudio(string scenario)
    {
        switch (scenario)
        {
            case "jumpSFX":
                sfxSource.PlayOneShot(jumpSFX);
                break;
            case "duckSFX":
                sfxSource.PlayOneShot(duckSFX);
                break;
            case "checkpointSFX":
                sfxSource.PlayOneShot(checkpointSFX);
                break;
            case "butterSFX":
                sfxSource.PlayOneShot(butterSFX);
                break;
            case "winSFX":
                StartCoroutine(PauseAudio());
                sfxSource.PlayOneShot(winSFX);
                
                break;
            default:
                Debug.Log("no audio found");
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator PauseAudio()
    {
        musicControlAudioSource.Pause();
        yield return new WaitForSeconds(2.5f);
        musicControlAudioSource.UnPause();
    }
}
