using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_PlayerDetectedState : PlayerDetectedState
{
    private Boss boss;
    public B_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData,Boss boss) : base(etity, stateMachine, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (performCloseRangeAction)
        {
            if (Time.time >= boss.dodgeState.startTime + boss.dogdeStateData.dodgeCooldown)
            {
                stateMachine.ChangeState(boss.dodgeState);
            }
            else
            {
                stateMachine.ChangeState(boss.meleeAttackState);
            }
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(boss.rangeAttack);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(boss.lookForPlayerState);
        }
        else if (!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(boss.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
