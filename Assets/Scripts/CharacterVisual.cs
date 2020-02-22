using UnityEngine;
using System.Collections;

public class CharacterVisual : MonoBehaviour
{
    public AudioController audioController;
    public AudioClip audioFootstep;

    public void PlayFootstepAudio()
    {
        audioController.PlayAudio(audioFootstep);
    }
}
