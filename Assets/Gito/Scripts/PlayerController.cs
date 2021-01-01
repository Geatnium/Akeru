using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviourUpdate
{
    private enum State
    {
        Stand, Crouch
    }

    private State state = State.Stand;
    [SerializeField] private Transform neck;
    [SerializeField] private float walkSpeed = 5.0f, dashSpeed = 10.0f, crouchSpeedMag = 0.5f;
    [SerializeField] private float mouseSensi = 1.0f, minMouseSensi = 0.1f, maxMouseSensi = 3.0f, stepMouseSensi = 0.2f;
    [SerializeField] private Transform crouchAxisTr;
    [SerializeField] private float crouchHeightMag = 0.5f, crouchingDulation = 0.3f;

    [SerializeField] private AudioClip footStepClip;
    [SerializeField] private float footStepVolumeBase = 0.5f;

    private CharacterController controller;
    private Animator animator;
    private AudioSource audioSource;
    private float heightOriginal;

    private Vector3 lookRot;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        heightOriginal = controller.height;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void UpdateM()
    {
        switch (Player.state)
        {
            case PlayerState.Freedom:
                Move();
                Crouch();
                MouseLook();
                break;
            case PlayerState.MouseLookOnly:
                MouseLook();
                break;
            case PlayerState.Event:
                lookRot.y = transform.eulerAngles.y;
                lookRot.x = neck.localEulerAngles.x;
                break;
            default:
                break;
        }
        animator.SetFloat("Velocity", controller.velocity.magnitude / walkSpeed);
    }

    // 移動
    public void Move()
    {
        // wasd
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // スピード
        float s = (Input.GetButton("Dash") ? dashSpeed : walkSpeed) * (state == State.Crouch ? crouchSpeedMag : 1.0f);
        // 進行方向を単位ベクトル化
        Vector3 dir = Vector3.Normalize(transform.forward * v + transform.right * h);
        // キャラコンで移動
        controller.SimpleMove(dir * s);

        audioSource.volume = controller.velocity.magnitude / walkSpeed * footStepVolumeBase;
    }

    // 視点移動
    public void MouseLook()
    {
        if (Input.GetButtonDown("UpDown"))
        {
            mouseSensi += Input.GetAxis("UpDown") * stepMouseSensi;
            if (mouseSensi <= minMouseSensi) mouseSensi = minMouseSensi;
            else if (mouseSensi >= maxMouseSensi) mouseSensi = maxMouseSensi;
        }
        // マウスの移動量と感度
        float mX = Input.GetAxis("Mouse X") * mouseSensi;
        float mY = Input.GetAxis("Mouse Y") * mouseSensi;
        // x軸は角度制限付き、y軸はそのまま回転
        lookRot = new Vector3(Mathf.Clamp(lookRot.x - mY, -90f, 90f), lookRot.y + mX, 0f);
        // y軸はプレイヤーを回転
        transform.eulerAngles = new Vector3(0, lookRot.y, 0);
        // x軸は首に
        neck.localEulerAngles = new Vector3(lookRot.x, 0, 0);
    }

    public void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            switch (state)
            {
                case State.Stand:
                    state = State.Crouch;
                    DOTween.To(
                        () => controller.height,
                        (x) => controller.height = x,
                        heightOriginal * crouchHeightMag,
                        crouchingDulation);
                    DOTween.To(
                        () => controller.center,
                        (x) => controller.center = x,
                        new Vector3(controller.center.x, heightOriginal * crouchHeightMag * 0.5f, controller.center.z),
                        crouchingDulation);
                    crouchAxisTr.DOLocalMoveY(-heightOriginal * crouchHeightMag, crouchingDulation);
                    break;
                case State.Crouch:
                    state = State.Stand;
                    DOTween.To(() => controller.height,
                        (x) => controller.height = x,
                        heightOriginal,
                        crouchingDulation);
                    DOTween.To(
                        () => controller.center,
                        (x) => controller.center = x,
                        new Vector3(controller.center.x, heightOriginal * 0.5f, controller.center.z),
                        crouchingDulation);
                    crouchAxisTr.DOLocalMoveY(0f, crouchingDulation);
                    break;
            }
        }
    }

    private void FootStepSound()
    {
        audioSource.PlayOneShot(footStepClip);
    }
}
