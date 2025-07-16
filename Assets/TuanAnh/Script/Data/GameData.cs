using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game/GameData", order = 1)]


public class GameData : ScriptableObject
{
    public int gameSpeed = 5;
    public float raycastDistance = 200f;
    public float minMoveDistance = 0.01f;
    public float minMoveSqrDistance = 0.01f;
    public float minStuckDistance = 0.0001f;
    public float maxMoveDistance = 20f;
    public float maxMoveSqrDistance = 400f;
    public float gravity = -9.81f;
    public float magicBallOffset = 1f;
    public float timeDurationForSpellTarget = 10f;
}
