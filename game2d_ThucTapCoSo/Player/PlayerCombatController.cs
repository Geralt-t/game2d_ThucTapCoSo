using System.Collections;
using System.Collections.Generic;
using Unity.Play.Publisher.Editor;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius,attack1Damage;
    [SerializeField]
    private Transform attack1HitBoxPos;
    private LayerMask whatIsDamageable;
    private bool gotInput,isAttacking,isFirstAttack;
    
    private float lastInputTime=Mathf.NegativeInfinity;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }
    private void Update()
    {
        CheckCompatInput();
    }
    private void CheckCompatInput()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }
    private void CheckAttacks()
    {
        if(gotInput)
        {
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack=!isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }
        if(Time.time >= lastInputTime+inputTimer)
        {
            gotInput=false;
        }
    }
    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position,attack1Radius,whatIsDamageable);
        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attack1Damage);

        }
    }
    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);   
    }
}
