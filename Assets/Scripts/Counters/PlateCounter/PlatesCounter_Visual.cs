using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter_Visual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform platePrefab;
    private List<Transform> plateList;
    private Transform removedPlate;

    private void Awake()
    {
        plateList = new List<Transform>();
    }

    private void Start()
    {
        plateCounter.OnPlatesSpawned += PlateCounter_OnPlatesSpawned;
        plateCounter.OnPlatesRemoved += PlateCounter_OnPlatesRemoved;
    }

    private void PlateCounter_OnPlatesRemoved(object sender, System.EventArgs e)
    {
        if (plateList.Count > 0)
        {
            int lastIndex = plateList.Count - 1;
            removedPlate = plateList[lastIndex];
            plateList.RemoveAt(lastIndex);
            Destroy(removedPlate.gameObject);
        }
    }

    private void PlateCounter_OnPlatesSpawned(object sender, System.EventArgs e)
    {
        Transform newSpawnedPlate = Instantiate(platePrefab, counterTopPoint);
        newSpawnedPlate.localPosition = new Vector3(0f, (plateList.Count * 0.1f), 0f);
        plateList.Add(newSpawnedPlate);
    }
}
