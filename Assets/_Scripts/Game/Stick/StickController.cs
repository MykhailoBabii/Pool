using System;
using Core.Utilities;
using DG.Tweening;
using Game.Provider;
using Game.Table;
using UI;
using UnityEngine;

namespace Game.Stick
{
    public interface IStickController
    {
        void Init();
        void PrepareOnReady();
        void ShowStick();
        void HideStick();
        void CalculateHitPower();
        void MakeStickDistanceAnimation();
        void SetStartDrawLinePosition();
        void InitOnShowComplete(Action action);
    }

    public class StickController : IStickController
    {
        private readonly ScreenView _screenView;
        private readonly ScreenTouchHandler _screenTouchHandler;
        private readonly PoolControllerBehaviour _poolControllerBehaviour;
        private readonly Settings _settings;

        private Vector3 _tuchPosition;
        private BallControllerBehaviour _mainBall;
        public FloatProperty StickDistancePosition = new(0);

        public StickController(
            ScreenView screenView,
            PoolControllerBehaviour poolControllerBehaviour,
            Settings settings,
            ScreenTouchHandler screenTouchHandler)
        {
            _screenView = screenView;
            _poolControllerBehaviour = poolControllerBehaviour;
            _settings = settings;
            _screenTouchHandler = screenTouchHandler;
        }

        public void Init()
        {
            StickDistancePosition.RemoveAllListeners();
            StickDistancePosition.AddValueChangedListenter(_poolControllerBehaviour.Stick.SetOffset);
            StickDistancePosition.AddValueChangedListenter(SetNormalPower);
            
            _screenTouchHandler.DragPoint.RemoveAllListeners();
            _screenTouchHandler.DragPoint.AddValueChangedListenter(MoveStick);
            _mainBall = _poolControllerBehaviour.MainBall;
        }

        private void SetNormalPower(float value)
        {
            var normalPower = Mathf.InverseLerp(_settings.MinTouchDistance, _settings.MaxTouchDistance, value);
            _screenView.SetSliderFill(normalPower);
        }

        public void PrepareOnReady()
        {
            var mainBallPosition = _mainBall.transform.position;
            var centerOfTable = _poolControllerBehaviour.CenterOfTablePoint.position;
            var rotation = _poolControllerBehaviour.Stick.SetCuePositionAndRotation(mainBallPosition, centerOfTable);
            _poolControllerBehaviour.TrajectoryDrawer.SetRotation(rotation);
            _poolControllerBehaviour.TrajectoryDrawer.SetPosition(mainBallPosition);
            _screenView.SetSliderPosition(_mainBall.gameObject);
        }

        public void CalculateHitPower()
        {
            var normalPower = Mathf.InverseLerp(_settings.MinTouchDistance, _settings.MaxTouchDistance, StickDistancePosition.Value);
            var power = _settings.AdditionalPower * normalPower;
            var ballPosition = _mainBall.transform.position;
            var direction = -(_tuchPosition - ballPosition).normalized;
            direction.y = 0;
            _mainBall.HitBall(direction, power);
        }

        public void ShowStick()
        {
            _poolControllerBehaviour.Stick.Show();
        }

        public void HideStick()
        {
            _poolControllerBehaviour.Stick.Hide();
        }

        public void MakeStickDistanceAnimation() // TODO
        {
            StickDistancePosition.SetValue(0);
            var ballPosition = _mainBall.transform.position;

            DOVirtual.Float(0, _settings.MinTouchDistance, 0.5f, value =>
            {
                StickDistancePosition.SetValue(value);
                _poolControllerBehaviour.Stick.SetCuePositionAndRotationOpposite(ballPosition, _tuchPosition);
                HideStick();
            });
        }

        public void SetStartDrawLinePosition()
        {
            _poolControllerBehaviour.TrajectoryDrawer.SetPosition(_mainBall.transform.position);
            _poolControllerBehaviour.TrajectoryDrawer.DrawTrajectory();
        }

        private void MoveStick(Vector3 touchPosition)
        {
            _tuchPosition = touchPosition;
            var ballPosition = _mainBall.transform.position;
            var distance = Vector3.Distance(_mainBall.transform.position, _tuchPosition);
            var stickPosition = Mathf.Clamp(distance * _settings.Stretch, _settings.MinTouchDistance, _settings.MaxTouchDistance);
            StickDistancePosition.SetValue(stickPosition);
            var rotation = _poolControllerBehaviour.Stick.SetCuePositionAndRotationOpposite(ballPosition, _tuchPosition);
            _poolControllerBehaviour.TrajectoryDrawer.SetRotation(rotation);
            _poolControllerBehaviour.TrajectoryDrawer.DrawTrajectory();
        }

        public void InitOnShowComplete(Action action)
        {
            _poolControllerBehaviour.Stick.InitOnShowComplete(action);
        }
    }
}


