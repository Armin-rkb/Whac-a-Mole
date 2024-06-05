using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    // The amount of possible moles which can appear.
    [SerializeField] int moleHoles;
    [SerializeField] float moleSpawnTimeSeconds = 1.5f;

    [SerializeField] private Mole molePrefab;
    [SerializeField] private Hole holePrefab;

    private bool canSpawnMoles = true;
    private Hole[] holes;

    void Start()
    {
        canSpawnMoles = false;
        SpawnHoles();
        StartCoroutine(MoleSpawnRoutine());
    }

    private void SpawnHoles()
    {
        holes = new Hole[moleHoles];
        for (int i = 0; i < moleHoles; i++)
        {
            float spawnPosX = Random.Range(-5, 5);
            float spawnPosY = Random.Range(-3, 2);
            Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY);

            Hole hole = Instantiate(holePrefab, spawnPos, Quaternion.identity);
            holes[i] = hole;
        }
        canSpawnMoles = true;
    }

    private void SpawnMole()
    {
        // Create a new list to find available holes for a mole to spawn.
        List<Hole> availableHoles = holes.Where(hole => !hole.IsOccupied).ToList();
        if (availableHoles.Count == 0)
        {
            // No available holes
            return;
        }

        Hole selectedHole = availableHoles[Random.Range(0, availableHoles.Count)];

        Mole mole = Instantiate(molePrefab, selectedHole.transform.position, Quaternion.identity);
        mole.SetHole(selectedHole);
        selectedHole.Occupy(mole);
    }

    private IEnumerator MoleSpawnRoutine()
    {
        while (canSpawnMoles)
        {
            yield return new WaitForSeconds(moleSpawnTimeSeconds);
            SpawnMole();
        }
    }
}
