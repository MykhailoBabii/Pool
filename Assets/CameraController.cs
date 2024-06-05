using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _padding;
    [SerializeField] private Vector2 _size;

    [ContextMenu("TestSize")]
    public void AdjustCameraSize()
    {
        float objectWidth = _size.x;
        float objectHeight = _size.y;

        float objectAspect = objectWidth / objectHeight;
        float cameraAspect = (float)Screen.width / Screen.height;

        if (cameraAspect >= objectAspect)
        {
            _camera.orthographicSize = (objectHeight / 2) * _padding;
        }
        else
        {
            _camera.orthographicSize = (objectWidth / 2 / cameraAspect) * _padding;
        }
    }
}
