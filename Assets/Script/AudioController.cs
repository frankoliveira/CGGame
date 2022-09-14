using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceMusicaDeFundo;
    public AudioClip [] musicasDeFundo;
    public bool menuMode = true;
    public bool playingMode = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Audio Controller:");
        Debug.Log("Menu:" + menuMode);
        Debug.Log("Playing:" + playingMode);

        if (playingMode)
        {
            audioSourceMusicaDeFundo = GetComponent<AudioSource>();
            AudioClip musicaDeFundoDessaFase = musicasDeFundo[0];
            audioSourceMusicaDeFundo.clip = musicaDeFundoDessaFase;
            audioSourceMusicaDeFundo.loop = true;
            audioSourceMusicaDeFundo.Play();
        }
        else if (menuMode)
        {
            AudioClip musicaDeFundoDessaFase = musicasDeFundo[1];
            audioSourceMusicaDeFundo.clip = musicaDeFundoDessaFase;
            audioSourceMusicaDeFundo.loop = true;
            audioSourceMusicaDeFundo.Play();
        }
    }
}
