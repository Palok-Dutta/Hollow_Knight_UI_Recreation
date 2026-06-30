using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using UnityEngine.UI;

public enum UIType
{
    MainMenu,
    GameStart,
    Options,
    Achievements,
    Quit,
}

public class PanelUI : MonoBehaviour
{
    [SerializeField] public UIType m_UIType;
    [SerializeField] public Button m_CorrespondingButton;
    
    [SerializeField] private OptionLoadAnimator[] m_OptionLoadAnimators;
    [SerializeField] private float m_Interval = 1f;
    [SerializeField] private UIType m_BackwardUIType;
    [SerializeField] private Button m_BackButton;
    private Sequence _sequence;

    private void Start()
    {
        if (m_BackButton == null) return;
        m_BackButton.onClick.AddListener(() =>
        {
            StopSequence();
        });
    }

    private void OnEnable()
    {
        PlaySequence();
    }

    private void OnDisable()
    {
        _sequence.Stop();
        foreach (var panel in m_OptionLoadAnimators)
        {
            panel.gameObject.SetActive(false);
        }
    }

    private void PlaySequence()
    {
        _sequence = Sequence.Create();
        for (var i = 0; i < m_OptionLoadAnimators.Length; i++)
        {
            var index = i;
            _sequence.ChainDelay(m_Interval).ChainCallback((() => m_OptionLoadAnimators[index].gameObject.SetActive(true)));
        }
    }

    private void StopSequence()
    {
        if (m_OptionLoadAnimators == null || m_OptionLoadAnimators.Length == 0)
        {
            OnAllReversed();
            return;
        }
        _sequence = Sequence.Create();
 
        const int lastIndex = 0; // loop runs Length-1 down to 0, so index 0 is the truly-last one to start
 
        for (var i = m_OptionLoadAnimators.Length - 1; i > -1; i--)
        {
            var index = i;
            var isLast = index == lastIndex;
 
            _sequence.ChainDelay(m_Interval).ChainCallback(() =>
            {
                m_OptionLoadAnimators[index].PlayReverseSequence(isLast ? OnAllReversed : null);
            });
        }
    }
 
    private void OnAllReversed()
    {
        gameObject.SetActive(false);
        MainMenuController.Instance.OnClick(m_BackwardUIType);
    }
}
