using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;

    void Start()
    {
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Vector3 pos = new Vector3(-7f + col * 2f, 3f - row * 0.6f, 0f);
                Instantiate(blockPrefab, pos, Quaternion.identity);
            }
        }
    }
}
