using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/* Description:
 * This class plays is responsible for playing musics, and if necessary keeps them playing after changing scenes.
 * */
public class MusicController : MonoBehaviour
{
    // --- PROPERTIES ---

    public static MusicController Instance;

    #region --- GENERAL CONFIG ---

    [Header("General Config")]

    [Tooltip("The music that will be played")]
        public AudioClip music;

    [HideInInspector]
    public string stageName = ""; //stores the current scene name
    public bool startPlaying = false;

    private AudioController audioController; //the AudioController that will play the musics
    protected bool loop;

    #endregion

    // --- MONOBEHAVIOUR METHODS ---

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        Init();

        if (startPlaying)
            PlayMusic();
    }

    /* Description:
     * Contains initial actions that are necessary for this class.
     * */
    public void Init()
    {
        stageName = SceneManager.GetActiveScene().name; //get the name of the current scene
        audioController = GetComponent<AudioController>(); //get the AudioController attached to this object

        loop = audioController.audioSource.loop;
    }

    /* Description:
     * Plays the music defined in the properties.
     * */
    public void PlayMusic()
    {
        //if a music is set, then play
        if (music != null)
        {
            audioController.PlayAudio(music, loop);
        }
    }

    /* Description:
     * Stop the current music from playing.
     * */
    public void StopMusic()
    {
        audioController.StopAudio();
    }

    /* Description:
     * Pause the current playing music.
     * */
    public void PauseMusic()
    {
        audioController.Pause();
    }

    /* Description:
     * Unpause the current playing music.
     * */
    public void UnpauseMusic()
    {
        audioController.Unpause();
    }

    public bool IsPlaying()
    {
        return audioController.IsPlaying();
    }
}
