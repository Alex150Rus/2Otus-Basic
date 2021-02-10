﻿using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        BeginAttack,
        Attack,
        BeginShoot,
        Shoot,
        BeginPunch,
        Punch,
        BeginDying,
        Dead,
    }

    public enum Weapon
    {
        Pistol,
        Bat,
        Fist,
    }

    public Weapon weapon;
    public Transform target;
    public TargetIndicator targetIndicator;
    public float runSpeed;
    public float distanceFromEnemy;
    public string damageSoundName = "DamageSound";
    public string takeDamageShotDistSoundName = "TakeDamageShortDist";
    public string takeDamageLongDistSoundName = "TakeDamageLongDist";
    public string ShootHitSoundName = "ShootHit";

    private PlaySound _playSound;
    private Animator animator;
    private State state;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Health health;
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Punch = Animator.StringToHash("Punch");
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int MeleeAttack = Animator.StringToHash("MeleeAttack");

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _playSound = GetComponentInChildren<PlaySound>();
        state = State.Idle;
        health = GetComponent<Health>();
        targetIndicator = GetComponentInChildren<TargetIndicator>(true);
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public bool IsIdle()
    {
        return state == State.Idle;
    }

    public bool IsDead()
    {
        return state == State.BeginDying || state == State.Dead;
    }

    public void SetState(State newState)
    {
        if (IsDead())
            return;

        state = newState;
    }

    public void TakeDamage()
    {
        if (IsDead())
            return;

        Character targetCharacter = target.GetComponent<Character>();

        switch(targetCharacter.weapon)
        {
            case Weapon.Pistol:
                if (_playSound) _playSound.Play(takeDamageLongDistSoundName);
                break;
            case Weapon.Bat:
            case Weapon.Fist:
                if (_playSound) _playSound.Play(takeDamageShotDistSoundName);
                break;
        }

        health.ApplyDamage(1.0f); // FIXME: захардкожено
        if (health.current <= 0.0f)
            state = State.BeginDying;
    }

    [ContextMenu("Attack")]
    public void AttackEnemy()
    {
        if (IsDead())
            return;

        Character targetCharacter = target.GetComponent<Character>();
        if (targetCharacter.IsDead())
            return;
        switch (weapon) {
            case Weapon.Bat:
                state = State.RunningToEnemy;
                break;
            case Weapon.Pistol:
                state = State.BeginShoot;
                break;
            case Weapon.Fist:
                state = State.RunningToEnemy;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.00001f) {
            transform.position = targetPosition;
            return true;
        }

        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 step = direction * runSpeed;
        if (step.magnitude < distance.magnitude) {
            transform.position += step;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }

    void FixedUpdate()
    {
        switch (state) {
            case State.Idle:
                transform.rotation = originalRotation;
                animator.SetFloat(Speed, 0.0f);
                break;

            case State.RunningToEnemy:
                animator.SetFloat(Speed, runSpeed);
                if (RunTowards(target.position, distanceFromEnemy)){
                    switch (weapon) {
                        case Weapon.Bat:
                            state = State.BeginAttack;
                            break;

                        case Weapon.Fist:
                            state = State.BeginPunch;
                            break;
                    }
                }
                break;

            case State.BeginAttack:
                animator.SetTrigger(MeleeAttack);
                state = State.Attack;
                break;

            case State.Attack:
                break;

            case State.BeginShoot:
                animator.SetTrigger(Shoot);
                if (_playSound) _playSound.Play(ShootHitSoundName);
                state = State.Shoot;
                break;

            case State.Shoot:
                break;

            case State.BeginPunch:
                animator.SetTrigger(Punch);
                state = State.Punch;
                break;

            case State.Punch:
                break;

            case State.RunningFromEnemy:
                animator.SetFloat(Speed, runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;

            case State.BeginDying:
                animator.SetTrigger(Death);
                state = State.Dead;
                break;

            case State.Dead:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
