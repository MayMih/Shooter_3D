using System.Collections;
using System.Collections.Generic;

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

    private BoxCollider selfCollider;

    private void Start()
    {
        selfCollider = GetComponent<BoxCollider>();
        StartCoroutine(SpawnRandomEnemy());
    }

    
    private void Update()
    {        
    }

    private IEnumerator SpawnRandomEnemy()
    {
        //const float MIN_DISTANCE_TO_GROUND = 0.1f;
        while (true)
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
            Physics.Raycast(obj.transform.position, Vector3.down, out hitInfo);
            //if (hitInfo.collider.CompareTag(groundTagSource.tag) && hitInfo.distance > MIN_DISTANCE_TO_GROUND)
            if (hitInfo.distance > 0)
            {
                var groundedPos = obj.transform.position;
                groundedPos.y -= hitInfo.distance * 1.7f;
                obj.transform.position = groundedPos;
            }
            //Debug.Log($"Trying to spawn enemy at {obj.transform.position}");
            obj.GetComponent<TargetController>().LifeTime = enemyLifeTime;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
