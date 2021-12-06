using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public bool isEnemyLive = true;

    public float distance;

    public LayerMask player;

    Animator anim;

    public Transform attackOverlappPos;
    public float attackOverlappRadius;

    public float enemySpeed;

    private float enemyRightX;
    private float enemyLeftX;

    Rigidbody2D rb;
    private bool right = true;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        enemyRightX = transform.position.x + 4;
        enemyLeftX = transform.position.x - 4;
    }

    void Update()
    {
        if (isEnemyLive)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance, player);

            if (hit.collider != null)
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);


                if (Mathf.Abs(hit.transform.position.x - transform.position.x) < 1.5f)
                {
                    anim.SetBool("Attack", true);

                }
                else
                {
                    anim.SetBool("Attack", false);
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.red);
            }


            if (right)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (transform.position.x < enemyRightX)
                {
                    //ilerle
                    rb.velocity = new Vector2(enemySpeed * Time.deltaTime, 0);
                }
                else
                    right = false;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if (transform.position.x > enemyLeftX)
                {
                    rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, 0);
                }
                else
                    right = true;
            }
        }

    }

    public void AttackPlayer()
    {
        if(CheckAttackingPlayer() != null)
        {
            CheckAttackingPlayer().gameObject.GetComponent<PlayerController>().GetHit();
        }
    }
    private Collider2D CheckAttackingPlayer()
    {
        return Physics2D.OverlapCircle(attackOverlappPos.position, attackOverlappRadius, player);
    }

    public void GetHit()
    {
        if (isEnemyLive)
        {
            Health--;
            if (Health >= 1)
            {
                anim.SetTrigger("GetHit");
            }
            else
            {
                anim.SetBool("Attack", false);
                anim.SetBool("Dead", true);
                Destroy(GetComponent<Collider2D>());
                Destroy(GetComponent<Rigidbody2D>());
                isEnemyLive = false;
            }
        }
        
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOverlappPos.position, attackOverlappRadius);
    }
}
