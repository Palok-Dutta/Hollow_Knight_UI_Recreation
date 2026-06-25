using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> m_HoverAudioSouces;
    public static MainMenuController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayHoverSound()
    {
        foreach (var audio in m_HoverAudioSouces)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
                return;
            }
        }
        m_HoverAudioSouces[0].Stop();
        m_HoverAudioSouces[0].Play();
    }
}
