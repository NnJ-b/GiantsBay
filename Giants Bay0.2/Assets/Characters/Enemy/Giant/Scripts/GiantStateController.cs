using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantStateController : MonoBehaviour
{
    public delegate void OnStateChange(EnemyStates state);
    public OnStateChange onStateChangeCallBack;
    PlayerController player;
    public EnemyStates enemyState;

    [Header("References")]
    public GiantMotor motor;

    private void Start()
    {
        player = PlayerController.instance;
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < motor.sightRange)
        {
            enemyState = EnemyStates.Follow;
            if (onStateChangeCallBack != null)
            {
                onStateChangeCallBack.Invoke(enemyState);
            }
        }
    }




}
public enum EnemyStates { idle, Follow, Attack }
