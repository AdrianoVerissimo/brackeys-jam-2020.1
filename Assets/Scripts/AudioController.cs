using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class description:
 * This class has methods that helps playing sounds, making it easier and more practical.
 * */
public class AudioController : MonoBehaviour {

    [Header("------- AudioController.cs -------")]

    // --- PROPERTIES ---

    [Tooltip("AudioSource that will play the sounds")]
        public AudioSource audioSource;

    public float startLoopTime = 0f;

    protected AudioClip audioClip = null;

    // --- METHODS ---

    /* Description:
     * Will play a specific sound.
     * 
     * Parameters:
     * AudioClip value      -> the sound that will be played
     * bool loop            -> if true, the sound will loop; if false, will not. Default is false
     * bool waitOtherFinish -> if true, will only play the sound when the previous one is finished; if false, will play immediately. Default is false
     * */
    public void PlayAudio(AudioClip value, bool loop = false, bool waitOtherFinish = false)
	{
        //no AudioSource is attached
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); //try to get an AudioSource from the object that this script is attached

            //didn't get any AudioSource, so stop the method execution
            if (audioSource == null)
            {
                print("You need to attach an AudioSource before continue.");
                return;
            }
        }


        //if it's checked to wait the sound to stop before playing a new one, and there is a sound playing, stop the method execution
		if (waitOtherFinish && audioSource.isPlaying)
			return;

        audioClip = value;

		//play the sound
		audioSource.loop = loop; //define the loop
		audioSource.clip = value; //define the audio clip to be played
		audioSource.Play ();
	}

    /* Description:
     * Stop the sound that is currently playing
     * */
    
    public void StopAudio()
    {
        audioSource.Stop();
    }

    /* Description:
     * Tells if there is a sound being played
     * */
	public bool IsPlaying()
	{
		return audioSource.isPlaying;
	}

    /* Description:
     * Pause the current sound
     * */
    virtual public void Pause()
    {
        audioSource.Pause();
    }

    /* Description:
     * Unpause the current sound
     * */
    virtual public void Unpause()
    {
        audioSource.UnPause();
    }

    public IEnumerator PlayNewStartPosition()
    {
        yield return new WaitForSeconds(audioClip.length);
        audioSource.time = startLoopTime;
        audioSource.Play();
    }
}
