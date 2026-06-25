using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject m_SideDesign;

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
