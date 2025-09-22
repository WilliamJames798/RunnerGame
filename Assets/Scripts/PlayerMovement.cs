using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float horizontalSpeed = 8f;
    [SerializeField] private float leftLimit = -9f; // 游戏区域左边界
    [SerializeField] private float rightLimit = 9f; // 游戏区域右边界
    [SerializeField] private float jumpForce = 25f; // 跳跃力度
    [SerializeField] private float slideDuration = 0.5f; // 滑行持续时间
    
    private bool isGrounded = true; // 是否在地面上
    private bool isSliding = false; // 是否在滑行中
    private float originalSpeed; // 记录玩家初始速度
    private CapsuleCollider capsuleCollider; // 玩家胶囊碰撞体
    private Vector3 originalColliderCenter; // 原始碰撞体中心
    private float originalColliderHeight; // 原始碰撞体高度
    private Rigidbody rb; // 玩家刚体
    private Animator animator; // 玩家动画控制器

    private void Start()
    {
        originalSpeed = moveSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalColliderCenter = capsuleCollider.center;
        originalColliderHeight = capsuleCollider.height;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // 冻结旋转，防止翻倒
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // 前进
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        // 向左移动
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
            }
        }

        // 向右移动
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
            }
        }

        // 检测是否在地面上（从角色中心往下射线）
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // 跳跃
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && !isSliding)
        {
            Jump();
        }

        // 滑行
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    // 跳跃方法
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        animator.SetTrigger("isJump"); // 播放跳跃动画
    }

    // 滑行协程
    private IEnumerator Slide()
    {
        isSliding = true;
        moveSpeed += 10f; // 滑行时增加速度
        animator.SetTrigger("isSlide"); // 播放滑行动画
        // 调整碰撞体以适应滑行动作
        capsuleCollider.height = originalColliderHeight / 2;
        capsuleCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y / 2, originalColliderCenter.z);
        yield return new WaitForSeconds(slideDuration); // 持续滑行一段时间
        // 恢复碰撞体
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;
        moveSpeed = originalSpeed; // 恢复原始速度
        isSliding = false;
    }

    // 加速协程
    public IEnumerator SpeedBoost(float boostAmount, float duration)
    {
        moveSpeed += boostAmount; // 增加速度
        Debug.Log("加速中：" + moveSpeed);
        yield return new WaitForSeconds(duration); // 持续一段时间
        moveSpeed = originalSpeed; // 恢复原始速度
        Debug.Log("加速结束，恢复速度：" + moveSpeed);
    }

    // 碰撞检测
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("碰到：" + collision.gameObject.name + "，Tag = " + collision.gameObject.tag);

        // 检查自身或父物体的 Tag
        if (collision.gameObject.CompareTag("Ground") || collision.transform.root.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("成功落地，可以跳跃");
        }
    }
}
