using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private AttackDetails attackDetails;
    public GameObject player;
    private Rigidbody rb;
    private float startTime;
    [SerializeField] private float timeActivated=1.5f;
    [SerializeField] private float damageRadius;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] Transform damagePosition;
    
    private void Start()
    {
        player = GameObject.Find("Character");
        transform.position = player.transform.position;
        startTime = Time.time;
        
    }
    private void FixedUpdate()
    {
        float currentTime = Time.time - startTime;

        Debug.Log("current" + (currentTime ));
        
        
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
        Debug.Log(damageHit);
        
        
        if (currentTime >  timeActivated)
        {
            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
            }
            Destroy(gameObject);
        }
    }
    public float ActivateDamage()
    {
        return Time.time;
    }
    public float DeActivateDamage()
    {
        return Time.time;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
    public void CastTheSpell(float damage,float timeActivate,float Radius)
    {
        attackDetails.damageAmount= damage;
        timeActivated = timeActivate;
        damageRadius = Radius;
    }
}
