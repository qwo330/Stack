using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Player : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    public float maxHP { get; protected set; }
    public float hp { get; protected set; }

    protected Vector3 bulletOffset = new Vector3(0f, 1f, 0f);
    float height = 0f;
    float moveSpeed = Defines.MOVE_SPEED;
    float jumpSpeed = Defines.JUMP_SPEED;

    bool isJump = false;
    bool isFirstTouch = false;
    float lastTouchTime;

    protected abstract void SetPlayer();
    public abstract void Skill_1();
    public abstract void Skill_2();
    public abstract void Skill_3();

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        SetPlayer();
        StartCoroutine(CO_Attack());
    }
    
    protected IEnumerator CO_Attack()
    {
        while (true)
        {
            if (InGameManager.Instance.CurrentState == GameState.Play)
            {
                Bullet bullet = ObjectPool.Get.GetObject(Defines.key_Bullet).GetComponent<Bullet>();
                bullet.Init(transform.position + bulletOffset);
            }
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
                anim.SetBool(Defines.key_RunL, false);
                anim.SetBool(Defines.key_RunR, false);
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
            anim.SetBool(Defines.key_Jump, true);
            height = transform.position.y + Defines.Jump_Height;
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

        if (Mathf.Abs(moveValue) > Vector3.kEpsilon + 0.1f)
        {
            Vector3 dir = Vector3.right;

            if (moveValue < 0)
            {
                dir = Vector3.right;
                anim.SetBool(Defines.key_RunR, true);
                anim.SetBool(Defines.key_RunL, false);
            }
            else
            {
                dir = Vector3.left;
                anim.SetBool(Defines.key_RunL, true);
                anim.SetBool(Defines.key_RunR, false);
            }

            Ray ray = new Ray(transform.position, -dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 0.5f))
            {
                if (hit.collider.CompareTag(Defines.key_Ground))
                    return;
            }

            transform.Translate(dir * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Defines.key_Ground) || rb.velocity.y == 0)
        {
            isJump = false;
            jumpSpeed = Defines.JUMP_SPEED;
            anim.SetBool(Defines.key_Jump, false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Defines.key_Enemy) || other.gameObject.CompareTag(Defines.key_EnemyBullet))
        {
            Damage(10);
        }
    }

    void Damage(float power)
    {
        hp = Mathf.Clamp(hp - power, 0, maxHP);
        InGameManager.Instance.playerHitEvent.Invoke(hp/maxHP);
        if (hp <= 0)
            InGameManager.Instance.GameOver();
    }
}