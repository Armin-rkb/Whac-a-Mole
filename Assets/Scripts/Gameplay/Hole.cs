using UnityEngine;

public class Hole : MonoBehaviour
{
    private bool isOccupied;

    // Todo: Could be removed?
    private Mole currentMole;

    public bool IsOccupied => isOccupied;

    public void Occupy(Mole mole)
    {
        isOccupied = true;
        currentMole = mole;
    }

    public void Vacate()
    {
        isOccupied = false;
        currentMole = null;
    }
}
