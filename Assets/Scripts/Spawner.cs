using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Chunk[] _chunkPrefabs;
    [SerializeField] private Chunk _firstChunk;

    private List<Chunk> spawnedChunks = new List<Chunk>();

    private void Start()
    {
        spawnedChunks.Add(_firstChunk);
    }

    private void Update()
    {
        if (_player != null && _player.position.z > spawnedChunks[spawnedChunks.Count - 1].EndPoint.position.z - 40f)
        {
            SpawnChunk();
            if (spawnedChunks.Count > 5)
            {
                Destroy(spawnedChunks[0]);
                spawnedChunks.RemoveAt(0);
            }
        }
    }

    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(_chunkPrefabs[Random.Range(0, _chunkPrefabs.Length)]);

        newChunk.transform.position = spawnedChunks[spawnedChunks.Count -1].EndPoint.position - newChunk.StartPoint.localPosition;

        spawnedChunks.Add(newChunk);
    }
}
