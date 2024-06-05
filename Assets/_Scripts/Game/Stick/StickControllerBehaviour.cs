using UnityEngine;
using DG.Tweening;

public class StickControllerBehaviour : MonoBehaviour
{
    [field: SerializeField] public float Offset { get; private set; } = 0.1f;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private GameObject _rootTransform;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _hightMovePosition;
    private Tweener _stickMoveAnimation;

    [ContextMenu("TestShow")]
    public void Show()
    {
        _stickMoveAnimation.Kill();
        _stickMoveAnimation = _rootTransform.transform.DOLocalMoveY(0, _animationTime);
        DOVirtual.Float(0, 1, _animationTime, value =>
        {
            var color = _meshRenderer.material.color;
            color.a = value;
            _meshRenderer.material.color = color;
        });
    }

    [ContextMenu("TestHide")]
    public void Hide()
    {
        _stickMoveAnimation.Kill();
        _stickMoveAnimation = _rootTransform.transform.DOLocalMoveY(_hightMovePosition, _animationTime);
        DOVirtual.Float(1, 0, _animationTime, value =>
        {
            var color = _meshRenderer.material.color;
            color.a = value;
            _meshRenderer.material.color = color;
        });
    }

    public Quaternion SetCuePositionAndRotation(Vector3 ballPosition, Vector3 tableCenter)
    {
        var direction = (tableCenter - ballPosition).normalized;
        var newPosition = ballPosition - direction * Offset;
        var rotation = Quaternion.LookRotation(direction);
        rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        transform.SetPositionAndRotation(newPosition, rotation);

        return rotation;
    }

    public Quaternion SetCuePositionAndRotationOpposite(Vector3 ballPosition, Vector3 tuchPosition)
    {
        var direction = (tuchPosition - ballPosition).normalized;
        var newPosition = ballPosition + direction * Offset;
        var rotation = Quaternion.LookRotation(-direction);
        rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        transform.SetPositionAndRotation(newPosition, rotation);

        return rotation;
    }

    internal void SetOffset(float offset)
    {
        Offset = offset;
    }
}
