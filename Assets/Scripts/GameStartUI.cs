using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private OptionLoadAnimator[] m_OptionLoadAnimators;
    [SerializeField] private float m_Interval = 1f;
    [SerializeField] private UIType m_BackwardUIType;
    [SerializeField] public UIType m_UIType;
    private Sequence _sequence;

    private void OnEnable()
    {
        PlaySequence();
    }

    private void OnDisable()
    {
        _sequence.Stop();
    }

    private void PlaySequence()
    {
        Debug.Log("PlaySequence started");
        _sequence = Sequence.Create();
        for (var i = 0; i < m_OptionLoadAnimators.Length; i++)
        {
            var index = i;
            _sequence.ChainDelay(m_Interval).ChainCallback((() => m_OptionLoadAnimators[index].gameObject.SetActive(true)));
        }
    }

    private void StopSequence()
    {
        Debug.Log("StopSequence started");
        _sequence =  Sequence.Create();
        for (var i = m_OptionLoadAnimators.Length -1; i > -1; i--)
        {
            var index = i;
            _sequence.ChainDelay(m_Interval).ChainCallback((() => m_OptionLoadAnimators[index].PlayReverseSequence()));
        }
        _sequence.OnComplete((() =>
        {
            this.gameObject.SetActive(false);
            MainMenuController.Instance.OnClick(m_BackwardUIType);
        }));
        
    }
}
