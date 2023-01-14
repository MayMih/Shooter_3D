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
    [SerializeField] LayerMask obstaclesLayerSource;
    
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
        var enemyPrefab = prefabsToSpawn[UnityEngine.Random.Range(0, prefabsToSpawn.Length)];
        var obj = Instantiate(enemyPrefab, transform);
        obj.transform.Rotate(Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up).eulerAngles);
        // случайная позиция внутри куба-рождения
        obj.transform.localPosition = new Vector3(Random.Range(-selfCollider.size.x / 2, selfCollider.size.x / 2),
            -selfCollider.size.y / 2,
            Random.Range(-selfCollider.size.z / 2, selfCollider.size.z / 2)
        );
        var filter = new ContactFilter2D();
        filter.SetLayerMask(obstaclesLayerSource);
        RaycastHit hitInfo;
        Physics.Raycast(obj.transform.position, -obj.transform.up, out hitInfo);
        Debug.DrawRay(obj.transform.position, -obj.transform.up, Color.red, 99);
        if (hitInfo.distance > 0)
        {
            var groundedPos = obj.transform.position;
            groundedPos.y -= hitInfo.distance * 1.7f;
            obj.transform.position = groundedPos;
        }
        //Debug.Log($"Trying to spawn enemy at {obj.transform.position}");
        obj.GetComponent<TargetController>().LifeTime = enemyLifeTime;
    }
}
