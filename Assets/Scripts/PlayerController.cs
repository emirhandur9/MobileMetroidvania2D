using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public float xInput;
    public float speed;

    public float groundOverlappRadius;
    public Transform groundOverlappPos;
    public LayerMask ground;

    public float attackOverlappRadius;
    public Transform attackOverlappPos;
    public LayerMask enemy;

    public float health;
    public float mana;

    public Image healthBar;
    public Image manaBar;

    private bool isMeditating;
    private bool isDefending;

    private int defendTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        #region run animation checker
        if (Mathf.Abs(xInput) > 0)
        {
            rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        #endregion

        #region jump animation checker
        if (rb.velocity.y > 0)
        {
            animator.SetBool("Jump", true);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);

            if (CheckPlayerGround())
            {
                animator.SetBool("Fall", false);
            }
        }
        #endregion

    }

    private void FixedUpdate()
    {
        if (isMeditating)
        {
            if (mana > 0)
            {
                animator.SetBool("Meditate", true);

                mana -= 0.1f; 
                manaBar.fillAmount = mana / 25;

                if(health < 25)
                {
                    health += 0.05f;
                    healthBar.fillAmount = health / 25;
                }
            }
            else
            {
                isMeditating = false;
                mana = 0;
                animator.SetBool("Meditate", false);
            }
        }

        if (isDefending)
        {
            defendTimer++;

            if (defendTimer > 10)
            {
                isDefending = false;
                defendTimer = 0;
            }
                
        }
    }


    public void Heal()
    {
        if(mana > 0)
        {
            isMeditating = true;
        }
    }

    public void Defend()
    {
        isMeditating = false;
        animator.SetTrigger("Defend");
    }
    public void Jump()
    {
        if (CheckPlayerGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, 7);
        }
        isMeditating = false;
        animator.SetBool("Meditate", false);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");

        if (CheckAttackingEnemy() != null)
        {
            Debug.Log("hit");
            CheckAttackingEnemy().gameObject.GetComponent<Enemy>().GetHit();
        }
        isMeditating = false;
        animator.SetBool("Meditate", false);
    }
    
    private bool CheckPlayerGround()
    {
        return Physics2D.OverlapCircle(groundOverlappPos.position, groundOverlappRadius, ground);
    }

    public void GetHit()
    {
        isMeditating = false;
        animator.SetBool("Meditate", false);

        if (!isDefending)
        {
            health--;
            animator.SetTrigger("GetHit");
            healthBar.fillAmount -= 0.04f;
        }
        
    }

    private Collider2D CheckAttackingEnemy()
    {
        return Physics2D.OverlapCircle(attackOverlappPos.position, attackOverlappRadius, enemy);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundOverlappPos.position, groundOverlappRadius);
        Gizmos.DrawWireSphere(attackOverlappPos.position, attackOverlappRadius);
    }
}
