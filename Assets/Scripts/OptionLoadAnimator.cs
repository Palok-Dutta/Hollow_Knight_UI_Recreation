using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class OptionLoadAnimator : MonoBehaviour
{
    [SerializeField] private Image m_TargetImage;
    [SerializeField] private Sprite[] m_FilledSprites;
    [SerializeField] private float m_StepInterval = 0.2f;
    [SerializeField] private Image m_ShowingNameImage;

    private Sequence _sequence;
    
    private void OnEnable()
    {
        PlaySequence();
    }
 
    private void OnDisable()
    {
        // Stop and clean up so re-enabling the panel always starts fresh
        // instead of resuming mid-sequence or stacking a second run.
        _sequence.Stop();
    }
 
    private void PlaySequence()
    {
        if (m_FilledSprites == null || m_FilledSprites.Length == 0)
        {
            return;
        }
 
        // Start on the first (emptiest) sprite immediately.
        m_TargetImage.sprite = m_FilledSprites[0];
 
        _sequence = Sequence.Create();
 
        for (var i = 1; i < m_FilledSprites.Length; i++)
        {
            var index = i; // local copy to avoid closure capture issues
            _sequence.ChainDelay(m_StepInterval)
                .ChainCallback(() => m_TargetImage.sprite = m_FilledSprites[index]);
        }
        
        if(m_ShowingNameImage == null) return;
        _sequence.OnComplete((() => m_ShowingNameImage.gameObject.SetActive(true)));
    }
    
    public void PlayReverseSequence(Action onComplete = null)
    {
        if (m_FilledSprites == null || m_FilledSprites.Length == 0)
        {
            Debug.LogWarning($"{nameof(OptionLoadAnimator)}: no fillSprites assigned.", this);
            onComplete?.Invoke();
            return;
        }
 
        // Stop any forward sequence that might still be running.
        _sequence.Stop();
 
        // Hide immediately - opposite of the OnComplete reveal in PlaySequence.
        if (m_ShowingNameImage != null)
            m_ShowingNameImage.gameObject.SetActive(false);
 
        // Start fully filled, immediately.
        m_TargetImage.sprite = m_FilledSprites[^1];
 
        _sequence = Sequence.Create();
 
        for (var i = m_FilledSprites.Length - 2; i >= 0; i--)
        {
            var index = i; // local copy to avoid closure capture issues
            _sequence.ChainDelay(m_StepInterval)
                .ChainCallback(() => m_TargetImage.sprite = m_FilledSprites[index]);
        }
 
        _sequence.OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }
}
