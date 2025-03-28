using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    private int _numberOfCheckpoints;
    private int _lapCount;
    private List<Checkpoint> _checkpoints;


    private void Start()
    {
        //_numberOfCheckpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None).Lenght;
    }
    public void AddCheckpoint(Checkpoint checkToAdd)
    {
        if (checkToAdd.isFinishLine)
        {
            FinishLap();
        }
        if(_checkpoints.Contains(checkToAdd) == false)
        {
            _checkpoints.Add(checkToAdd);
        }
    }

    private void FinishLap()
    {
        if (_checkpoints.Count > _numberOfCheckpoints / 2)
        {
            _lapCount++;
            _checkpoints.Clear();
            if (_lapCount >= 3)
            {

            }
        }
    }
}
