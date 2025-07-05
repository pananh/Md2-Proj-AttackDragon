using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "Game/GameSetting", order = 1)]


public class GameSetting : ScriptableObject
{
    public int gameSpeed = 5;
    public float raycastDistance = 200f;
    public float minMoveDistance = 0.01f;
    public float minMoveSqrDistance = 0.01f;
    public float minStuckDistance = 0.0001f;
    public float maxMoveDistance = 20f;
    public float maxMoveSqrDistance = 400f;
    public float gravity = -9.81f;
    public Vector3 magicBallLocalOffset = new Vector3(0, 1, 2);
    public float timeDurationForSpellTarget = 10f;
}
