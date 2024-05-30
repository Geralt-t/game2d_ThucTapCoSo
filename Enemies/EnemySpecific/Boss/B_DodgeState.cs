using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_DodgeState : DodgeState
{
    private Boss boss;

    public B_DodgeState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_DogdeStateData stateData,Boss boss) : base(etity, stateMachine, animBoolName, stateData)
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
        if(isDodgeOver)
        {
            if(isPlayerInMaxAgroRange&&performCloseRangeAction)
            {
                stateMachine.ChangeState(boss.meleeAttackState);
            }
            else if(!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(boss.lookForPlayerState);
            }
            else if(isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(boss.rangeAttack);
            }
            //TODO:RAnge attack state
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
