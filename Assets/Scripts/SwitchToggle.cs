using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backgroundActiveColor;
    [SerializeField] Color handleActiveColor;

    Image backgroundImage, handleImage;

    Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle;

    Vector2 handlePosition;

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

    void OnSwitch (bool on)
    {
        uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);

        backgroundImage.DOColor(on ? backgroundActiveColor : backgroundDefaultColor, .6f);

        handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, .6f);

        //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor;

        //handleImage.color = on ? handleActiveColor : handleDefaultColor;

        //if (on)
        //    uiHandleRectTransform.anchoredPosition = handlePosition * -1;

        //else
        //    uiHandleRectTransform.anchoredPosition = handlePosition;

    }

   void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}

// Script from Hamza Herbou's YouTube tutorial "Unity Custom UI Toggle"
// https://www.youtube.com/watch?v=fRqqef8246Q