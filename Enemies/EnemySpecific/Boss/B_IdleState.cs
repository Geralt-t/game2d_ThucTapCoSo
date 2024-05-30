using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_IdleState : IdleState
{
    private Boss boss;

    public B_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Boss boss) : base(etity, stateMachine, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(boss.playerDetectedState);
        }
        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(boss.moveState);

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
