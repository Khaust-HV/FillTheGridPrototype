using System.Collections;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace CameraControllers {
    public sealed class CameraZoomController : MonoBehaviour, IControlZoomTheCamera {
        #region Camera Configs
            private float _sensitivityZoom;
            private float _zoomDuration;
            private float _minZoomScale;
            private float _maxZoomScale;
        #endregion

        private float _oldDoubleInputDistance;

        private Vector3 _targetPosition;

        private bool isZoomActive;
        private IEnumerator _zoomStarted;
        private float _currentTimeToStop;

        #region DI
            private CameraConfigs _cameraConfigs;
        #endregion

        [Inject]
        private void Construct(CameraConfigs cameraConfigs) {
            // Set DI
            _cameraConfigs = cameraConfigs;

            // Set configurations
            _sensitivityZoom = cameraConfigs.SensitivityZoom;
            _zoomDuration = cameraConfigs.ZoomDuration;
            _minZoomScale = cameraConfigs.MinZoomScale;
            _maxZoomScale = cameraConfigs.MaxZoomScale;
        }

        public void SetZoomingActive(bool isActive) {
            if (isActive) {
                _currentTimeToStop = Time.time + _zoomDuration;

                if (!isZoomActive) StartCoroutine(_zoomStarted = ZoomStarted());
            } else if (isZoomActive) {
                StopCoroutine(_zoomStarted);

                isZoomActive = false;
            }
        }

        private IEnumerator ZoomStarted() {
            isZoomActive = true;

            float zoomingSmoothSpeed = _cameraConfigs.ZoomingSmoothSpeed;

            while (_currentTimeToStop > Time.time) {
                var stepPosition = Vector3.Lerp(transform.position, _targetPosition, zoomingSmoothSpeed);

                var currentPosition = transform.position;

                stepPosition = new Vector3 (
                    currentPosition.x, 
                    stepPosition.y,
                    currentPosition.z
                );

                transform.position = stepPosition;

                yield return null;
            }
            
            isZoomActive = false;
        }

        public void SetDoubleInputDistance(float distance) {
            Vector3 position = transform.position;

            if (_oldDoubleInputDistance < distance) position = position + Vector3.down * _sensitivityZoom;
            else position = position + Vector3.up * _sensitivityZoom;

            var valueConsideringMinAndMaxHeight = Mathf.Clamp(position.y, _minZoomScale, _maxZoomScale);

            _targetPosition = new Vector3(position.x, valueConsideringMinAndMaxHeight, position.z); 

            _oldDoubleInputDistance = distance;
        }
    }   
}

public interface IControlZoomTheCamera {
    public void SetZoomingActive(bool isActive);
    public void SetDoubleInputDistance(float distance);
}