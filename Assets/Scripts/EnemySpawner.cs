using System.Collections;
using System.Collections.Generic;

using Assets.Scripts;

using UnityEngine;

/// <summary>
/// ������ ��� ������� �������� ������
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("�������� ����� ���������� ������ (� ��������)")]
    [SerializeField] float spawnDelay = 0.5f;
    [Header("����� ������ ����� (� ��������)")]
    [SerializeField] float enemyLifeTime = 2;

    [SerializeField] GameObject[] prefabsToSpawn;
    [SerializeField] LayerMask obstaclesLayerSource;

    private BoxCollider selfCollider;

    private void Start()
    {
        selfCollider = GetComponent<BoxCollider>();
        StartCoroutine(SpawnRandomEnemyRoutine());
    }


    private IEnumerator SpawnRandomEnemyRoutine()
    {        
        while (true)
        {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void SpawnRandomEnemy()
    {
        //const float MIN_DISTANCE_TO_GROUND = 0.1f;
        var enemyPrefab = prefabsToSpawn[UnityEngine.Random.Range(0, prefabsToSpawn.Length)];
        //var enemyPrefab = prefabsToSpawn[0];
        var obj = Instantiate(enemyPrefab, transform);
        obj.transform.Rotate(Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up).eulerAngles);
        // ��������� ������� ������ ����-��������
        obj.transform.localPosition = new Vector3(Random.Range(-selfCollider.size.x / 2, selfCollider.size.x / 2),
            -selfCollider.size.y / 2,
            Random.Range(-selfCollider.size.z / 2, selfCollider.size.z / 2)
        );
        var filter = new ContactFilter2D();
        filter.SetLayerMask(obstaclesLayerSource);
        RaycastHit hitInfo;
        Physics.Raycast(obj.transform.position, -obj.transform.up, out hitInfo);
        Debug.DrawRay(obj.transform.position, -obj.transform.up, Color.red, 99);
        //if (hitInfo.collider.CompareTag(groundTagSource.tag) && hitInfo.distance > MIN_DISTANCE_TO_GROUND)
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
