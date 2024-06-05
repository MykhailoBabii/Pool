using System;
using Core.States;
using Core.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI
{

    public class ScreenView : MonoBehaviour
    {
        [Inject] private readonly IStateMachine<ApplicationStates> _applicationStateMachine;

        [SerializeField] private Button _resetButton;
        [SerializeField] private PowerSlider _powerSlider;
        [SerializeField] private Image _fadeImage;


        void Awake()
        {
            _resetButton.onClick.AddListener(OnResetTapHandler);
        }

        public void SetSliderFill(float value)
        {
            _powerSlider.SetFill(value);
        }

        public void SetSliderPosition(GameObject ball)
        {
            var uiPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, ball.transform.TransformPoint(Vector3.zero));

            _powerSlider.SetPosition(uiPosition);
        }

        public void ShowLine(bool isShow)
        {
            _powerSlider.gameObject.SetActive(isShow);
        }

        public void FadeAnimation()
        {
            _fadeImage.raycastTarget = true;
            _fadeImage.DOFade(1, 0);
            _fadeImage.DOFade(0, 1).OnComplete(() => _fadeImage.raycastTarget = false);
        }

        private void OnResetTapHandler()
        {
            _applicationStateMachine.SwitchToState(ApplicationStates.Start);
        }
    }
}
