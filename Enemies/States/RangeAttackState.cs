using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackData stateData;
    protected GameObject spell;
    protected Spell spellScript;
    public RangeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackData stateData) : base(etity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        spell=GameObject.Instantiate(stateData.spell,attackPosition.position,attackPosition.rotation);
        spellScript=spell.GetComponent<Spell>();
        spellScript.CastTheSpell(stateData.spellDamage, stateData.timeActivated, stateData.damageRadius);
    }
}
