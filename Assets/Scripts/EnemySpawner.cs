using System.Collections;

using Assets.Scripts;

using UnityEngine;

/// <summary>
/// Скрипт для области создания врагов
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("Интервал между появлением врагов (в секундах)")]
    [SerializeField] float spawnDelay = 0.5f;
    [Header("Время показа врага (в секундах)")]
    [SerializeField] float enemyLifeTime = 2;
    [SerializeField] GameObject[] prefabsToSpawn;
    // временно не используется, т.к. вроде бы не было замечено проваливание внутрь объектов
    //[SerializeField] LayerMask obstaclesLayerSource;
    
    public AudioClip enemySelfDestructSound;
    public AudioClip enemyRespawnSound;

    private BoxCollider selfCollider;
    private UIHandler uiHandlerScript;
    

    private void Awake()
    {
        uiHandlerScript = GameObject.FindObjectOfType<UIHandler>();
        selfCollider = GetComponent<BoxCollider>();        
    }

    public void Begin()
    {
        StartCoroutine(SpawnRandomEnemyRoutine());
    }


    private IEnumerator SpawnRandomEnemyRoutine()
    {        
        while (!uiHandlerScript.IsGameOver)
        {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    /// <summary>
    /// Метод генерации врага на случайной позиции внутри Куба-рождения
    /// </summary>
    public void SpawnRandomEnemy()
    {
        var enemyPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        var randRot = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
        // случайная позиция внутри куба-рождения
        var localRandPos = new Vector3(Random.Range(-selfCollider.size.x / 2, selfCollider.size.x / 2),
            -selfCollider.size.y / 2,
            Random.Range(-selfCollider.size.z / 2, selfCollider.size.z / 2)
        );
        var globalRandPos = transform.TransformPoint(localRandPos);
        var obj = Instantiate(enemyPrefab, globalRandPos, randRot);        
        // временно не используется, т.к. вроде бы не было замечено проваливание внутрь объектов
        //var filter = new ContactFilter2D();
        //filter.SetLayerMask(obstaclesLayerSource);
        RaycastHit hitInfo;
        Physics.Raycast(obj.transform.position, -obj.transform.up, out hitInfo);
        //
        Debug.DrawRay(obj.transform.position, -obj.transform.up, Color.red, 99);
        //
        if (hitInfo.distance > 0)
        {            
            var landingPos = obj.transform.position;
            landingPos.y -= hitInfo.distance;
            obj.transform.position = landingPos;
            Debug.Log($"New Enemy {obj} landed on {hitInfo.collider.name}");
        }
        //Debug.Log($"Trying to spawn enemy at {obj.transform.position}");
        var target = obj.GetComponent<TargetController>();
        target.SoundPlayer = GetComponent<AudioSource>();
        target.EnemyRespawnSound = enemyRespawnSound;
        target.SelfDestructSound = enemySelfDestructSound;
        target.LifeTime = enemyLifeTime;        
    }
}
