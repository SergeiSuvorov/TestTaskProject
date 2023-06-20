using Tools;
using UniRx;
using UnityEngine;

namespace Controller
{
    public sealed class CameraController:BaseController
    {
        private readonly GameObject _targetGameObject;
        private readonly Camera _camera;

        private readonly float _xCameraBoard;
        private readonly float _yCameraBoard;


        private float speed = 1.5f;
        private float xOffset = 1.5f;
        private float yOffset = 5f;
        private float journeyLength;

        private bool _isMoving;

        public CameraController(GameObject targetGameObject, CompositeDisposable disposables, float xSize, float ySize)
        {
            _targetGameObject = targetGameObject;
            _camera = Camera.main;
           
            var rightUpCoordinate = _camera.ViewportToWorldPoint(Vector2.one);

            _xCameraBoard = xSize-rightUpCoordinate.x;
            _yCameraBoard = ySize-rightUpCoordinate.y;

            _camera.transform.position = new Vector3(_targetGameObject.transform.position.x, _targetGameObject.transform.position.y, _camera.transform.position.z);

            Observable.EveryUpdate().Subscribe(_ => { Update(); }).AddTo(disposables);
        }

        public void Update()
        {
            if (CheckPosition())
            {
                Vector3 endPosition = _camera.transform.position.Change(x: _targetGameObject.transform.position.x, y: _targetGameObject.transform.position.y);
                endPosition = new Vector3(Mathf.Clamp(endPosition.x, -_xCameraBoard, _xCameraBoard), Mathf.Clamp(endPosition.y, -_yCameraBoard, _yCameraBoard), endPosition.z);
                journeyLength = Vector3.Distance(_camera.transform.position, endPosition);
                float distCovered = Time.deltaTime * speed;
                float fractionOfJourney = journeyLength > 2 ? distCovered : (distCovered / journeyLength);
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, endPosition, fractionOfJourney);
            }

        }

        private bool CheckPosition()
        {
            if (Mathf.Abs(_camera.transform.position.x - _targetGameObject.transform.position.x) > xOffset
                || Mathf.Abs(_camera.transform.position.y - _targetGameObject.transform.position.y) > yOffset)
            {
                _isMoving = true;
                return true;
            }
            else if ((Mathf.Abs(_camera.transform.position.x - _targetGameObject.transform.position.x) < xOffset
                || Mathf.Abs(_camera.transform.position.y - _targetGameObject.transform.position.y) < yOffset) && _isMoving)
                return true;
            else
                return false;
        }
    }
 }