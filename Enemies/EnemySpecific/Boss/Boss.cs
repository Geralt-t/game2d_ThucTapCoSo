using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Boss : Entity
{
   
    public B_IdleState idleState { get; private set; }
    public B_MoveState moveState { get; private set; }
    public B_PlayerDetectedState playerDetectedState { get; private set; }
    public B_MeleeAttackState meleeAttackState { get; private set; }
    public B_LookForPlayerState lookForPlayerState { get; private set; }
    public B_DeadState deadState { get; private set; }
    public B_ChargeState chargeState { get; private set; }
    public B_DodgeState dodgeState { get; private set; }
    public B_RangeAttack rangeAttack { get;private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    public D_DogdeStateData dogdeStateData;
    [SerializeField]
    public D_RangeAttackData rangeAttackData;
    

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangeAttackPosition;
    public override void Start()
    {
        base.Start();
        moveState = new B_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new B_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new B_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new B_MeleeAttackState(this, stateMachine, "meleeAttack",meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new B_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        deadState = new B_DeadState(this, stateMachine, "dead", deadStateData, this);
        chargeState = new B_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        dodgeState = new B_DodgeState(this, stateMachine, "dodge", dogdeStateData, this);
        rangeAttack = new B_RangeAttack(this, stateMachine, "rangeAttack",rangeAttackPosition ,rangeAttackData,this);

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
        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if(CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangeAttack);
        }

    }
    
}
