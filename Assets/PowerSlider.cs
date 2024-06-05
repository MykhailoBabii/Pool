using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;

public class PowerSlider : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _maskImage;
    [SerializeField] private float _lineOffset;
    public void Show()
    {
        _maskImage.DOFade(1, 1);
    }

    public void Hide()
    {
        _maskImage.DOFade(0, 0);
    }

    public void SetFill(float value)
    {
        _fillImage.fillAmount = value;
    }

    internal void SetPosition(Vector3 screenPosition)
    {
        screenPosition.y -= _lineOffset;
        transform.position = screenPosition;
    }
}
