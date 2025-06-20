using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
   public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetected playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; } 
    public E1_MeeleAttackState meeleAttackState { get; private set; }
    public E1_DeadState deadState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    
    
        [SerializeField]
    private Transform meleeAttackPosition;
    
    
    public override void Start()
    {
        base.Start();
        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetected(this, stateMachine, "playerDetected",playerDetectedData,this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meeleAttackState = new E1_MeeleAttackState(this, stateMachine, "meleeAttack",meleeAttackPosition,meleeAttackStateData,this);
        deadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);
        stateMachine.Initialize(moveState);
        
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if(isDead)
        {
            stateMachine.ChangeState(deadState);
        }
       
    }
}
