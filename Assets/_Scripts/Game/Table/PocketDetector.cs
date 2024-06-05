using System;
using UnityEngine;
using static Lofelt.NiceVibrations.HapticPatterns;

namespace Game.Table
{
    public class PocketDetector : MonoBehaviour
    {
        private Action _mainBallIsDetectedAction;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BallControllerBehaviour>(out var ball))
            {
                if (ball.IsMain == true)
                {
                    _mainBallIsDetectedAction?.Invoke();
                }

                else
                {
                    _audioSource.PlayOneShot(_audioSource.clip);
                    PlayPreset(PresetType.MediumImpact);
                    ShowParticle(ball.transform.position);
                    ball.OnDestroyBall();
                }
            }
        }

        public void InitOnMainBallIsDetected(Action action)
        {
            _mainBallIsDetectedAction = action;
        }

        private void ShowParticle(Vector3 position)
        {
            var newPosition = new Vector3(position.x, _particleSystem.transform.position.y, position.z);
            _particleSystem.transform.position = newPosition;
            _particleSystem.Play();
        }
    }
}
