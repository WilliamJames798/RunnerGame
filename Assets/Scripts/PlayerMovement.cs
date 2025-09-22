using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float horizontalSpeed = 8f;
    [SerializeField] private float leftLimit = -9f; // ��Ϸ������߽�
    [SerializeField] private float rightLimit = 9f; // ��Ϸ�����ұ߽�
    [SerializeField] private float jumpForce = 25f; // ��Ծ����
    [SerializeField] private float slideDuration = 0.5f; // ���г���ʱ��
    
    private bool isGrounded = true; // �Ƿ��ڵ�����
    private bool isSliding = false; // �Ƿ��ڻ�����
    private float originalSpeed; // ��¼��ҳ�ʼ�ٶ�
    private CapsuleCollider capsuleCollider; // ��ҽ�����ײ��
    private Vector3 originalColliderCenter; // ԭʼ��ײ������
    private float originalColliderHeight; // ԭʼ��ײ��߶�
    private Rigidbody rb; // ��Ҹ���
    private Animator animator; // ��Ҷ���������

    private void Start()
    {
        originalSpeed = moveSpeed;
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalColliderCenter = capsuleCollider.center;
        originalColliderHeight = capsuleCollider.height;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // ������ת����ֹ����
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // ǰ��
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        // �����ƶ�
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
            }
        }

        // �����ƶ�
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
            }
        }

        // ����Ƿ��ڵ����ϣ��ӽ�ɫ�����������ߣ�
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // ��Ծ
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && !isSliding)
        {
            Jump();
        }

        // ����
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    // ��Ծ����
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        animator.SetTrigger("isJump"); // ������Ծ����
    }

    // ����Э��
    private IEnumerator Slide()
    {
        isSliding = true;
        moveSpeed += 10f; // ����ʱ�����ٶ�
        animator.SetTrigger("isSlide"); // ���Ż��ж���
        // ������ײ������Ӧ���ж���
        capsuleCollider.height = originalColliderHeight / 2;
        capsuleCollider.center = new Vector3(originalColliderCenter.x, originalColliderCenter.y / 2, originalColliderCenter.z);
        yield return new WaitForSeconds(slideDuration); // ��������һ��ʱ��
        // �ָ���ײ��
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;
        moveSpeed = originalSpeed; // �ָ�ԭʼ�ٶ�
        isSliding = false;
    }

    // ����Э��
    public IEnumerator SpeedBoost(float boostAmount, float duration)
    {
        moveSpeed += boostAmount; // �����ٶ�
        Debug.Log("�����У�" + moveSpeed);
        yield return new WaitForSeconds(duration); // ����һ��ʱ��
        moveSpeed = originalSpeed; // �ָ�ԭʼ�ٶ�
        Debug.Log("���ٽ������ָ��ٶȣ�" + moveSpeed);
    }

    // ��ײ���
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("������" + collision.gameObject.name + "��Tag = " + collision.gameObject.tag);

        // ������������� Tag
        if (collision.gameObject.CompareTag("Ground") || collision.transform.root.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("�ɹ���أ�������Ծ");
        }
    }
}
