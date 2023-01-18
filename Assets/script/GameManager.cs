using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("GameObject")] 
    [SerializeField] private GameObject completLevelUI;
    [SerializeField] private GameObject butter;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject butterSpawn;
    private bool wonGame;
    #endregion
    [HideInInspector] public AudioSource sfxSource;
    public static AudioClip duckSFX, jumpSFX, checkpointSFX;
    private void Start()
    {
        wonGame = false;
        try
        {
            completLevelUI = GameObject.FindGameObjectWithTag("WinScreen");
            completLevelUI.SetActive(false);
            Debug.Log(("found win screen"));
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("Can't find object");
        }
        GetSpawns();
        InstantiateObjects();
    }
    private void GetSpawns()
    {
        try
        {
            playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
            butterSpawn = GameObject.FindGameObjectWithTag("ButterSpawn");
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("Can't find object");
        }
    }
    private void InstantiateObjects()
    {
        try
        {
            Instantiate(butter, butterSpawn.gameObject.transform.position, butterSpawn.gameObject.transform.rotation);
        } 
        catch (System.NullReferenceException)
        {
            Debug.Log("Instantiated objects not defined yet (Spawn Butter)");
        }
    }

    public void EndLevel()
    {
        Debug.Log("toastt");
    }
    public void CompleteLevel()
    {
        wonGame = true;
        if (wonGame)
        {
            completLevelUI.SetActive(true);
        }
    }
}
