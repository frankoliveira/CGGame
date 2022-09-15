using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceMusicaDeFundo;
    public AudioClip [] musicasDeFundo;
    public bool menuMode = true;
    public bool playingMode = false;

    void Start()
    {
        audioSourceMusicaDeFundo = GetComponent<AudioSource>();

        if (playingMode)
        {
            AudioClip musicaDeFundoDessaFase = musicasDeFundo[0];
            audioSourceMusicaDeFundo.clip = musicaDeFundoDessaFase;
            audioSourceMusicaDeFundo.loop = true;
            audioSourceMusicaDeFundo.Play();
        }
        else if (menuMode)
        {
            AudioClip musicaDeFundoDessaFase = musicasDeFundo[2];
            audioSourceMusicaDeFundo.clip = musicaDeFundoDessaFase;
            audioSourceMusicaDeFundo.loop = true;
            audioSourceMusicaDeFundo.Play();
        }
    }
}
