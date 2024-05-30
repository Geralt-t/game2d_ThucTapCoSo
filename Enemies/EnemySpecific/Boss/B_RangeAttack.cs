using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_RangeAttack : RangeAttackState
{
    private Boss boss;

    public B_RangeAttack(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackData stateData,Boss boss) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.boss = boss;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAnimationFinish)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(boss.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(boss.lookForPlayerState);
            }
        }    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
