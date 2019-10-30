using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameSystem.ObjectPool;
using Managers.InGameManager;
using UnityEngine.EventSystems;

namespace Objects.Player
{
    public abstract class Player : MonoBehaviour
    {
        public UISprite HPSprite;

        InGameManager igm;
        Rigidbody rb;
        Animator anim;

        [SerializeField]
        protected float maxHP, HP;

        float height = 0f;
        float moveSpeed = Defines.MOVESPEED;
        float jumpSpeed = Defines.JUMPSPEED;

        bool isJump = false;

        bool isFirstTouch = false;
        float lastTouchTime = 0.0f;

        void Start()
        {
            igm = GameObject.FindWithTag("GameController").GetComponent<InGameManager>();
            if (igm == null)
            {
                Debug.Log("인게임 컨트롤러를 찾을 수 없음");
                return;
            }

            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();

            setPlayer();
            StartCoroutine(attack());
        }

        protected abstract void setPlayer();

        IEnumerator attack()
        {
            while ((igm.isGameOver | igm.isClear) == false)
            {
                yield return new WaitForSeconds(Defines.ATTACKINTERVAL);
                GameObject bullet = InGameManager.Instance.Pool.GetObject(ObjectType.Bullet);
                bullet.transform.position = transform.position + new Vector3(0f, 1f, 0f);
            }
        }

        void Update()
        {
            GameObject obj = UICamera.hoveredObject;
            if (obj != null)
            {
                if (!obj.CompareTag("TouchableUI"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        jumpCheck();
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        move();
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        anim.SetBool("RunL", false);
                        anim.SetBool("RunR", false);
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if (isJump)
            {
                jump();
            }
        }

        void jumpCheck()
        {
            if ((Time.time - lastTouchTime) < 0.25f && !isJump)
            {
                isJump = true;
                anim.SetBool("Jump", true);
                height = transform.position.y + 3f;
            }
            lastTouchTime = Time.time;
        }

        void jump()
        {
            if (transform.position.y < height)
                transform.Translate(0, jumpSpeed * Time.deltaTime, 0);
            else
                jumpSpeed = 0;
        }

        void move()
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float moveValue = touchPos.x - transform.position.x;

            if (Mathf.Abs(moveValue) > 0.1f)
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
            Rigidbody rb = GetComponent<Rigidbody>();

            if (collision.gameObject.CompareTag("Ground") || rb.velocity.y == 0)
            {
                isJump = false;
                jumpSpeed = Defines.JUMPSPEED;
                anim.SetBool("Jump", false);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyBullet"))
                demage();
        }

        void demage()
        {
            HP--;
            HPSprite.fillAmount = HP / maxHP;

            if (HP <= 0)
                igm.GameOver();
        }

        public abstract void Skill1();
        public abstract void Skill2();
        public abstract void Skill3();
    }
}