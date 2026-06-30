using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private List<AudioSource> m_HoverAudioSouces;
    public static MainMenuController Instance;
    [SerializeField] private PanelUI[] m_Panels;
    private void Awake()
    {
        Instance = this;
    }
    
    private void WireUpButtons()
    {
        foreach (var panel in m_Panels)
        {
            var type = panel.m_UIType; // capture for the closure
            if (panel.m_CorrespondingButton == null) continue;
            panel.m_CorrespondingButton.onClick.AddListener(() => OnClick(type));
        }
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

    private void Start()
    {
        HideAll();
        DefaultOn();
        WireUpButtons();
    }

    private void DefaultOn()
    {
        m_Panels[0].gameObject.SetActive(true);
    }

    public void OnClick(UIType type)
    {
        HideAll();
        switch (type)
        {
            case  UIType.GameStart:
                m_Panels[1].gameObject.SetActive(true);
                break;
            case  UIType.MainMenu:
                m_Panels[0].gameObject.SetActive(true);
                break;
            case  UIType.Options:
                m_Panels[2].gameObject.SetActive(true);
                break;
            case  UIType.Achievements:
                m_Panels[3].gameObject.SetActive(true);
                break;
            case  UIType.Quit:
                m_Panels[4].gameObject.SetActive(true);
                break;
        }
    }

    private void HideAll()
    {
        foreach (var panel in m_Panels)
        {
            panel.gameObject.SetActive(false);
        }
    }
}
