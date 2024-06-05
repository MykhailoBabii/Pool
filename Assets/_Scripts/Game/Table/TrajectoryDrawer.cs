using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    [SerializeField] private int _maxReflections = 5;
    [SerializeField] private float _maxStepDistance = 200f;
    [SerializeField] private float _maxWidth;
    [SerializeField] private float _animationTime;
    [SerializeField] private LayerMask _collisionLayers;
    [SerializeField] private LineRenderer _lineRenderer;


    public void Show()
    {
        DOVirtual.Float(0, _maxWidth, _animationTime, value => _lineRenderer.widthMultiplier = value);
    }

    public void Hide()
    {
        _lineRenderer.widthMultiplier = 0;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void DrawTrajectory()
    {
        List<Vector3> points = new();
        Vector3 startPosition = transform.position;
        Vector3 direction = transform.forward;

        points.Add(startPosition);

        for (int i = 0; i < _maxReflections; i++)
        {
            Ray ray = new(startPosition, direction);

            if (Physics.Raycast(ray, out var hit, _maxStepDistance, _collisionLayers))
            {
                startPosition = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                points.Add(startPosition);
            }
            else
            {
                points.Add(ray.GetPoint(_maxStepDistance));
                break;
            }
        }

        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }
}
