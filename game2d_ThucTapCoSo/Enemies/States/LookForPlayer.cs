using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayer : State
{
    protected D_LookForPlayer stateData;
    protected bool TurnImidiately;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;
    protected float lastTurnTime;
    protected int amountOfTurnsDone;
    public LookForPlayer(Entity etity, FiniteStateMachine stateMachine, string animBoolName,D_LookForPlayer stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange=entity.CheckPlayerInMinAgroRange();

    }

    public override void Enter()
    {
        base.Enter();
        isAllTurnsDone=false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (TurnImidiately)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            TurnImidiately = false;
        }
        else if(Time.time>=lastTurnTime+stateData.timeBetweenTurns&&!isAllTurnsDone)
        {
            entity.Flip();
            lastTurnTime=Time.time;
            amountOfTurnsDone++;
        }
        if(amountOfTurnsDone>=stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }
        if(Time.time>=lastTurnTime+stateData.timeBetweenTurns&& isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SetTurnImidiately(bool flip)
    {
        TurnImidiately = flip;
    }
}
