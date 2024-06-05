using UnityEngine;

public class BallControllerBehaviour : MonoBehaviour
{
    [field: SerializeField] public bool IsMain { get; private set; }
    [field: SerializeField] public Rigidbody Rb { get; private set; }
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _minVelocity = 0.05f;
    [SerializeField] private float _maxVelocity = 1.2f;

    public void StopVelocity()
    {
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
    }

    public void HitBall(Vector3 direction, float power)
    {
        Rb.velocity = direction * power;
    }

    public void PlayHitAudio()
    {
        _audioSource.Play();
    }

    public void SetColor(Color color)
    {
        _meshRenderer.materials[1].color = color;
    }

    public void OnDestroyBall()
    {
        StopVelocity();
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Rb.velocity.magnitude < _minVelocity)
        {
            StopVelocity();
        }

        _audioSource.volume = Mathf.InverseLerp(_minVelocity, _maxVelocity, Rb.velocity.magnitude);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallControllerBehaviour>(out var ball))
        {
            PlayHitAudio();
        }
    }
}
