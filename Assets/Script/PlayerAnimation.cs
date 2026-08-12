using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Lấy tín hiệu di chuyển từ phím A/D, W/S hoặc Mũi tên
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Nếu có bấm phím di chuyển -> isRunning = true
        bool isMoving = (horizontal != 0 || vertical != 0);
        anim.SetBool("isRunning", isMoving);

        // Bấm phím Space để nhảy
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Jump");
        }
    }
}