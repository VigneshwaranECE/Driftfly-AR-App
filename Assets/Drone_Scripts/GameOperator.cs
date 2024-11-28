using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameOperator : MonoBehaviour
{
    public DroneController _DroneController;
    public Button _FlyButton;
    public Button _LandButton;

    public GameObject _Controls;

    public ARRaycastManager _RaycastManager;
    public ARPlaneManager _PlaneManager;
    List<ARRaycastHit> _HitResult = new List<ARRaycastHit>();

    public GameObject _Drone;

    struct DroneAnimationControls
    {
        public bool moving;
        public bool interpolatingAsc;
        public bool interpolatingDesc;
        public float _axis;
        public float _direction;
    }

    DroneAnimationControls _MovingLeft;
    DroneAnimationControls _MovingBack;
    void Start()
    {
        _FlyButton.onClick.AddListener(EventOnClickFlyButton);
        _LandButton.onClick.AddListener(EventOnClickLandButton);
    }

    
    void Update()
    {
        //float speedX = Input.GetAxis("Horizontal");
        //float speedZ = Input.GetAxis("Vertical");

        UpdateControls(ref _MovingLeft);
        UpdateControls(ref _MovingBack);

        _DroneController.Move(_MovingLeft._axis * _MovingLeft._direction, _MovingBack._axis * _MovingBack._direction);

        if(_DroneController.IsIdle())
        {
            UpdateAR();
        }
    }

    void UpdateAR()
    {
        Vector2 positionScreenSpace = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

        _RaycastManager.Raycast(positionScreenSpace, _HitResult, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds);
        
        if(_HitResult.Count > 0)
        {
            if (_PlaneManager.GetPlane(_HitResult[0].trackableId).alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.HorizontalUp)
            {
                Pose pose = _HitResult[0].pose;
                _Drone.transform.position = pose.position;
                _Drone.SetActive(true);
            }
        }
    }

    void UpdateControls(ref DroneAnimationControls _controls)
    {
        if(_controls.moving || _controls.interpolatingAsc || _controls.interpolatingDesc)
        {
            if(_controls.interpolatingAsc)
            {
                _controls._axis += 0.05f;

                if(_controls._axis >= 1.0f)
                {
                    _controls._axis = 1.0f;
                    _controls.interpolatingAsc = false;
                    _controls.interpolatingDesc = true;
                }
            }
            else if(!_controls.moving)
            {
                _controls._axis -= 0.05f;

                if (_controls._axis <= 0.0f)
                {
                    _controls._axis = 0.0f;
                    _controls.interpolatingDesc = false;

                }
            }
        }
    }

    void EventOnClickFlyButton()
    {
        if(_DroneController.IsIdle())
        {
            _DroneController.TakeOff();
            _FlyButton.gameObject.SetActive(false);
            _Controls.SetActive(true);
        }
    }
    void EventOnClickLandButton()
    {
        if(_DroneController.IsFlying())
        {
            _DroneController.Land();
            _FlyButton.gameObject.SetActive(true);
            _Controls.SetActive(false);
        }
    }

    public void EventOnLeftButtonPressed()
    {
        _MovingLeft.moving = true;
        _MovingLeft.interpolatingAsc = true;
        _MovingLeft._direction = -1.0f;
    }
    public void EventOnLeftButtonReleased()
    {
        _MovingLeft.moving = false;
        
    }
    public void EventOnRightButtonPressed()
    {
        _MovingLeft.moving = true;
        _MovingLeft.interpolatingAsc = true;
        _MovingLeft._direction = 1.0f;
    }
    public void EventOnRightButtonReleased()
    {
        _MovingLeft.moving = false;
    }
    public void EventOnBackButtonPressed()
    {
        _MovingBack.moving = true;
        _MovingBack.interpolatingAsc = true;
        _MovingBack._direction = -1.0f;
    }
    public void EventOnBackButtonReleased()
    {
        _MovingBack.moving = false;
    }
    public void EventOnForwardButtonPressed()
    {
        _MovingBack.moving = true;
        _MovingBack.interpolatingAsc = true;
        _MovingBack._direction = 1.0f;
    }
    public void EventOnForwardButtonReleased()
    {
        _MovingBack.moving = false;
    }
  }

