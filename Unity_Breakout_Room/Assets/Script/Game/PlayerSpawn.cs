using System.Collections.Generic;
using UnityEngine;


public class PlayerSpawn : MonoBehaviour
{
    public List<Transform> spawnPostions;
    public GameObject prefab;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Instantiate(prefab, spawnPostions[0].position, Quaternion.Euler(0,180,0));
        }
    }
}
