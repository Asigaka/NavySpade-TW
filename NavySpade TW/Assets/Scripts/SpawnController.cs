using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private MapControler map;
    [SerializeField] private SessionScreen sessionScreen;

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    [Header("Crystal")]
    [SerializeField] private Crystal crystalPrefab;
    [SerializeField] private Transform crystalContainer;

    [Header("Enemy")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private List<SpawnZone> enemySpawnZones;
    [SerializeField] private Transform enemiesContainer;

    [Header("Debug")]
    [SerializeField] private List<GameObject> spawnedCrystals;
    [SerializeField] private List<GameObject> spawnedEnemies;

    private void Start()
    {
        spawnedCrystals = new List<GameObject>();
        spawnedEnemies = new List<GameObject>();

        SpawnObjects(crystalPrefab.gameObject, ref spawnedCrystals, crystalContainer);
        SpawnObjects(enemyPrefab.gameObject, ref spawnedEnemies, enemiesContainer, enemySpawnZones);
    }

    private void Update()
    {
        if (PlayerController.Instance)
        {
            sessionScreen.SetNearestEnemy(GetDistanceToNearest(spawnedEnemies));
            sessionScreen.SetNearestCrystal(GetDistanceToNearest(spawnedCrystals));
        }
    }

    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, playerPrefab.transform.rotation);
    }

    private void SpawnObjects(GameObject prefab, 
        ref List<GameObject> spawnedList, Transform container, List<SpawnZone> spawnZones = null)
    {
        if (prefab.GetComponent<ISpawnable>() == null)
        {
            Debug.LogWarning("Префаб должен реализовывать ISpawnable!");
            return;
        }

        ISpawnable spawnable = prefab.GetComponent<ISpawnable>();

        spawnedList = ObjectPooler.Instance.FillContainer(prefab,
            container, spawnable.StartCount, spawnable.MaxSpawnedCountOnMap);
        StartCoroutine(CheckAndFill(spawnable, spawnedList, spawnZones));
        SetPositions(spawnedList, spawnZones);
        SetDisappearence(spawnedList);
    }

    private IEnumerator CheckAndFill(ISpawnable spawnable, 
        List<GameObject> spawnedList, List<SpawnZone> spawnZones = null)
    {
        while (true)
        {
            float period = Random.Range(spawnable.MinSpawnPeriodicity, spawnable.MaxSpawnPeriodicity);
            yield return new WaitForSeconds(period);

            GameObject currentObj = ObjectPooler.Instance.GetNotActiveObjectFromList(spawnedList);

            if (currentObj != null)
            {
                currentObj.SetActive(true);

                if (spawnZones == null)
                    currentObj.transform.position = map.GetRandomPosOnGround();
                else
                    currentObj.transform.position = map.GetRandomPosOnSpawnZone(spawnZones);
            }
        }
    }

    private void SetPositions(List<GameObject> spawnedObjects, List<SpawnZone> spawnZones)
    {
        foreach (GameObject currentObj in spawnedObjects)
        {
            if (spawnZones == null)
                currentObj.transform.position = map.GetRandomPosOnGround();
            else
                currentObj.transform.position = map.GetRandomPosOnSpawnZone(spawnZones);
        }
    }

    private void SetDisappearence(List<GameObject> spawnedObjects)
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            IActiveChanger disappearenceObj = spawnedObjects[i].GetComponent<IActiveChanger>();

            if (disappearenceObj != null)
            {
                disappearenceObj.OnActiveChange.AddListener(OnChangeDisappearence);
            }
        }
    }

    private int GetActiveObjectsCount(List<GameObject> list)
    {
        int actvive = 0;
        foreach (GameObject listObj in list)
        {
            if (listObj.activeSelf)
            {
                actvive++;
            }
        }

        return actvive;
    }

    private void OnChangeDisappearence()
    {
        int enemies = GetActiveObjectsCount(spawnedEnemies);
        int crystals = GetActiveObjectsCount(spawnedCrystals);
        sessionScreen.UpdateEnemiesAndCrystalsCount(crystals, enemies);
    }

    private int GetDistanceToNearest(List<GameObject> list)
    {
        Transform player = PlayerController.Instance.transform;

        list.Sort(delegate (GameObject b1, GameObject b2)
        {
            return Vector3.Distance(b1.transform.position, player.position)
            .CompareTo(Vector3.Distance(b2.transform.position, player.position));
        });

        return (int)Vector3.Distance(list[0].transform.position, player.position);
    }
}
