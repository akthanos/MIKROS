using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

    public String parameterName;

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = uiHandleRectTransform.anchoredPosition;

        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        handleImage = uiHandleRectTransform.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }
    public void OnSwitch(bool on)
    {
        uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);

        backgroundImage.DOColor(on ? backgroundActiveColor : backgroundDefaultColor, .6f);

        handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, .6f);

        if (on)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, 1.0f);
        }
        else
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, 0f);
        }
    }

   void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}

// Script from Hamza Herbou's YouTube tutorial "Unity Custom UI Toggle"
// https://www.youtube.com/watch?v=fRqqef8246Q
