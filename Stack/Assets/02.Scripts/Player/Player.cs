using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Player : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    public float maxHP { get; private set; }
    public float hp { get; private set; }

    protected Vector3 bulletOffset = new Vector3(0f, 1f, 0f);
    float height = 0f;
    float moveSpeed = Defines.MOVE_SPEED;
    float jumpSpeed = Defines.JUMP_SPEED;

    bool isJump = false;

    bool isFirstTouch = false;
    float lastTouchTime = 0.0f;

    protected abstract void SetPlayer();
    public abstract void Skill1();
    public abstract void Skill2();
    public abstract void Skill3();

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        SetPlayer();
        StartCoroutine(CO_Attack());
    }
    
    protected IEnumerator CO_Attack()
    {
        while ((InGameManager.Instance.isGameOver | InGameManager.Instance.isClear) == false)
        {
            GameObject bullet = ObjectPool.Get.GetObject(Defines.key_Bullet);
            bullet.transform.position = transform.position + bulletOffset;
            yield return new WaitForSeconds(Defines.ATTACK_INTERVAL);
        }
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckJump();
            }
            else if (Input.GetMouseButton(0))
            {
                Move();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("RunL", false);
                anim.SetBool("RunR", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (isJump)
        {
            Jump();
        }
    }

    void CheckJump()
    {
        if ((Time.time - lastTouchTime) < 0.25f && !isJump)
        {
            isJump = true;
            anim.SetBool("Jump", true);
            height = transform.position.y + 3f;
        }
        lastTouchTime = Time.time;
    }

    void Jump()
    {
        if (transform.position.y < height)
            transform.Translate(0, jumpSpeed * Time.deltaTime, 0);
        else
            jumpSpeed = 0;
    }

    void Move()
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float moveValue = touchPos.x - transform.position.x;

        if (Mathf.Abs(moveValue) > Vector3.kEpsilon)
        {
            if (moveValue < 0)
            {
                Ray ray = new Ray(transform.position, Vector3.left);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 0.5f))
                {
                    if (hit.collider.CompareTag("Ground")) return;
                }

                transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);

                anim.SetBool("RunR", true);
                anim.SetBool("RunL", false);
            }
            else
            {
                Ray ray = new Ray(transform.position, Vector3.right);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 0.5f))
                {
                    if (hit.collider.CompareTag("Ground")) return;
                }

                transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);

                anim.SetBool("RunL", true);
                anim.SetBool("RunR", false);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || rb.velocity.y == 0)
        {
            isJump = false;
            jumpSpeed = Defines.JUMP_SPEED;
            anim.SetBool("Jump", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet"))
            Demage();
    }

    void Demage()
    {
        hp--;
        InGameManager.Instance.playerHitEvent.Invoke(hp/maxHP);
        if (hp <= 0)
            InGameManager.Instance.GameOver();
    }
}