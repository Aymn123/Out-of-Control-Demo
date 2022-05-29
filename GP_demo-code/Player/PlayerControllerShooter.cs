using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class PlayerControllerShooter : MonoBehaviour
{


    //Aim
    [Header("Aim")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float freeSinsevity = 1f;
    [SerializeField] private float adsSinsevity = 0.5f;
    [SerializeField] private float RotationSpeed = 10f;


    [SerializeField] private LayerMask aimCollider = new LayerMask();

    [SerializeField] private Transform headTarget;

    // [SerializeField] private Transform vfxHit;



    private Input _input;
    private PlayerController _playerController;

    private Transform _mainCamera;

    // [SerializeField] private Transform targetHead;
    [SerializeField] Rig RigAim;
    [SerializeField] float _RigSpeed;
    float accToAim;
    
    [SerializeField] Rig RigHand;
    [SerializeField] float _RigSpeedTo;
    float accToHand;



    Animator _animator;
    AK _Ak;
    public Vector3 target;

    public Transform hitTransform=null;
    [SerializeField] private Transform vfxHit;
    private void Awake()
    {
        _mainCamera = Camera.main.transform;
        _input = GetComponent<Input>();
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _Ak = GetComponent<AK>();
    }
    public RaycastHit raycastHit;
    private void Update()
    {
        Vector2 screemCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screemCenterPoint);

        
        if (Physics.Raycast(ray, out raycastHit, 999f, aimCollider))
        {
            headTarget.position = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        
        if (_input.Shoot|| _input.Aim)
        {
            if(_input.Aim)
            aimVirtualCamera.gameObject.SetActive(true);
            _playerController.setSinsevity(adsSinsevity);
            _playerController.SetRotateOnMove(false);

            float tangetAngle = _mainCamera.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, tangetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
            if (_input.Shoot)
            {
                _Ak.Shoot();
            }

            accToHand = Mathf.Lerp(RigHand.weight, 0, Time.deltaTime * _RigSpeedTo);
            RigHand.weight = 0;

            accToAim = Mathf.Lerp(RigAim.weight, 1, Time.deltaTime * _RigSpeed);
             RigAim.weight = accToAim;
            _animator.SetLayerWeight(1, accToAim);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            _playerController.setSinsevity(freeSinsevity);
            _playerController.SetRotateOnMove(true);

            accToHand = Mathf.Lerp(RigHand.weight, 1, Time.deltaTime * _RigSpeedTo);
            RigHand.weight = accToHand;

            accToAim = Mathf.Lerp(RigAim.weight, 0, Time.deltaTime * _RigSpeed);
            RigAim.weight = accToAim;
           _animator.SetLayerWeight(1, accToAim);
        }






        


    }
}

