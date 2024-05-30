using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetected : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetected(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        if(performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meeleAttackState);
        }
        else if(performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if(!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else if(!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
