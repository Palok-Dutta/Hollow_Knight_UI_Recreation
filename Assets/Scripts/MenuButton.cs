using System;
using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject m_SideDesign;
    [SerializeField] private Button m_Button;
    
    [SerializeField] private RectTransform m_ButtonRect;
    [SerializeField] private RectTransform m_GlowRect;   
    [SerializeField] private CanvasGroup m_CanvasGroup;
    [SerializeField] private float m_Duration = 0.5f;
 
    private void TriggerGlow()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_ButtonRect, Input.mousePosition, null, out var localPoint);
        m_GlowRect.anchoredPosition = localPoint;
        Glow();
    }
 
    private void Glow()
    {
        m_CanvasGroup.alpha = 0f;
        Sequence.Create()
            .Chain(Tween.Alpha(m_CanvasGroup, 1f, m_Duration))
            .Chain(Tween.Alpha(m_CanvasGroup, 0f, m_Duration));
    }
    private void Start()
    {
        m_Button.onClick.AddListener(MainMenuController.Instance.PlayClickSound);
        m_Button.onClick.AddListener(TriggerGlow);
    }

    private void OnEnable()
    {
        m_SideDesign.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_SideDesign.SetActive(true);
        MainMenuController.Instance.PlayHoverSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_SideDesign.SetActive(false);
    }
}    
