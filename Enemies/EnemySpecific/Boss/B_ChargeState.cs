using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_ChargeState : ChargeState
{
    private Boss boss;
    public B_ChargeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Boss boss) : base(etity, stateMachine, animBoolName, stateData)
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
            stateMachine.ChangeState(boss.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(boss.lookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            if (isPlayerinMinAgroRange)
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
}
