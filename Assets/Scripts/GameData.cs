using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains numerical data 
//NOT GENERATED SUCH AS RAND NUMS
public class GameData
{

    // GENERAL GAME DATA

    public enum Dimension
    { Arcade = 2,
        Modern = 3 } // Arcade will be 2 Dimensional and Modern will be 3 Dimensional
    public static Dimension currentDimension = Dimension.Modern; // setting this to Modern as starting version 
    public static bool switchedToArcade = false;


    public static float despawnTimeAfterPass = 0.5f;
    public static KeyCode switchDimensionsKey = KeyCode.Tab;
    public static float arcadeAcceptableY = 2.0f;

    public static float arcadeBarMax = 50f;
    public static float arcadeBarRegenRate = 1f;
    public static float arcadeBarUseRate = 20f;

    


    // BULLET DATA
    public static float despawnInTime = 0.5f;
    public static float bulletOffsetPlayerZ = 10f;
    public static float bulletSpeed = 500f;
    public static float bulletDmg = 5.0f;

    // CAMERA DATA
    public static Vector3 camOffSetPosModern = new Vector3(0f, 3f, -20f);
    public static Vector3 camOffSetPosArcade = new Vector3(0f, 150f, 20f);
    public static Vector3 camRotArcade = new Vector3(90f, 0f, 0f); //euler rotation
    public static float camSmoothSpeed = 1f;

    // SPAWNED OBJECTS DATA

    public static Vector3 minSpawnPos = new Vector3(0,0,100);
    public static float passingDistBeforeDespawn = 50f;
    public static Vector2Int spawnRangeX = new Vector2Int(-100, 100);
    public static Vector2Int spawnRangeY = new Vector2Int(-100, 100);
    public static Vector2Int spawnRangeZ = new Vector2Int(-100, 100);

    public static Vector2 randYArcade = new Vector2(0, 0);

    // SMALL ENEMY DATA
    public static float shootDelay = 0.5f;
    public static int enemyDirection = -1;
    public static float agroRange = 100f;

    // ATTACK DATA
    public static string defaultWeaponTag = "Gun";

    // VISUAL EFFECT DATA
    public static string thrusterTag = "Thruster";

    //public static GameObject obstaclePrefab;
    public static float obstacleHealth = 20.0f;
    public static Vector3 obstacle;

    public static ArrayList obstaclePositions = new ArrayList(); //will hold all obstacle positions

    

    
}
