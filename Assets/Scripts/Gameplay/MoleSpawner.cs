using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    // The amount of possible moles which can appear.
    [SerializeField] int moleHoles;
    [SerializeField] float minMoleSpawnTime = 0.5f;
    [SerializeField] float maxMoleSpawnTime= 1.5f;

    [Header("Hole Spawn Location")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 2;
    [SerializeField] private float randomOffset = 0.5f;
    [Header("Safe area in which the holes will spawn")]
    [SerializeField] private Vector2 topLeftCorner = new Vector2(-5, 2);
    [SerializeField] private Vector2 bottomRightCorner = new Vector2(5, -3);

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

        float boxWidth = bottomRightCorner.x - topLeftCorner.x;
        float boxHeight = topLeftCorner.y - bottomRightCorner.y;

        float spacingX = boxWidth / (columns - 1);
        float spacingY = boxHeight / (rows - 1);

        int i = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // Calculate the spawn point position
                Vector2 position = new Vector3(
                    topLeftCorner.x + column * spacingX + Random.Range(-randomOffset, randomOffset),
                    topLeftCorner.y - row * spacingY + Random.Range(-randomOffset, randomOffset)
                );

                // Ensure the position stays within the bounds
                position.x = Mathf.Clamp(position.x, topLeftCorner.x, bottomRightCorner.x);
                position.y = Mathf.Clamp(position.y, bottomRightCorner.y, topLeftCorner.y);

                // Instantiate the spawn point
                Hole hole = Instantiate(holePrefab, position, Quaternion.identity);
                holes[i] = hole;
                i++;
            }
        }
        canSpawnMoles = true;
    }

    private void SpawnMole()
    {
        if (holes[0] == null)
            return;

        // Create a new list to find available holes for a mole to spawn.
        List<Hole> availableHoles = holes.Where(hole => !hole.IsOccupied).ToList();
        if (availableHoles.Count == 0)
        {
            // No available holes
            return;
        }

        Hole selectedHole = availableHoles[Random.Range(0, availableHoles.Count)];

        Mole mole = Instantiate(molePrefab, selectedHole.transform.position, Quaternion.identity);
        selectedHole.Occupy(mole);
        mole.SetHole(selectedHole);
        mole.PopUp();
    }

    private IEnumerator MoleSpawnRoutine()
    {
        while (canSpawnMoles)
        {
            yield return new WaitForSeconds(Random.Range(minMoleSpawnTime, maxMoleSpawnTime));
            SpawnMole();
        }
    }
}
