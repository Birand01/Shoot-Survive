using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private GameObject[] turretModels;
    [SerializeField] private Vector3 turretPosition;
    private void Awake()
    {
        ChooseTurretModel(SaveManager.instance.currentTurret);
    }

    private void ChooseTurretModel(int _index)
    {
        Instantiate(turretModels[_index], turretPosition, Quaternion.identity,transform);
    }
}
