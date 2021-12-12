using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Chunk[] _fetchPrefabs;
    [SerializeField] private Chunk _firstChunk;
    [SerializeField] private Chunk _treeChunk;

    [SerializeField] private List<Chunk> _spawnedChunks = new List<Chunk>();
    // Start is called before the first frame update
    private void Start()
    {
        _spawnedChunks.Add(_firstChunk);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_player.position.y>_spawnedChunks[_spawnedChunks.Count - 1]._end.position.y-5)
        {
            SpawnChunk();
        }
    }
    private void SpawnChunk()
    {
        if (_spawnedChunks.Count==1)
        {
            DefaultChunk();
        }
        else
        {
            RandomizeChunk();
        }
        


        if (_spawnedChunks.Count >= 11)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }
    private void DefaultChunk()
    {
        Chunk newChunk;
        for (int i = 0; i < 1; i++)
        {
            newChunk = Instantiate(_treeChunk);
            newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1]._end.position - newChunk._begin.localPosition;
            newChunk.transform.Rotate(new Vector3(0, _spawnedChunks[_spawnedChunks.Count - 1].transform.rotation.eulerAngles.y + Random.Range(-30, 30), 0));
            _spawnedChunks.Add(newChunk);
        }
        newChunk = Instantiate(_fetchPrefabs[0]);
        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1]._end.position - newChunk._begin.localPosition;
        newChunk.transform.Rotate(new Vector3(0, _spawnedChunks[_spawnedChunks.Count - 1].transform.rotation.eulerAngles.y + Random.Range(-30, -10), 0));
        _spawnedChunks.Add(newChunk);
    }
    private void RandomizeChunk()
    {
        Chunk newChunk;
        for (int i = 0; i < 1; i++)
        {
            newChunk = Instantiate(_treeChunk);
            newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1]._end.position - newChunk._begin.localPosition;
            newChunk.transform.Rotate(new Vector3(0, _spawnedChunks[_spawnedChunks.Count - 1].transform.rotation.eulerAngles.y + Random.Range(-30, -10), 0));
            _spawnedChunks.Add(newChunk);
        }
        newChunk = Instantiate(_fetchPrefabs[0]);
        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1]._end.position - newChunk._begin.localPosition;
        newChunk.transform.Rotate(new Vector3(0, _spawnedChunks[_spawnedChunks.Count - 1].transform.rotation.eulerAngles.y, 0));
        _spawnedChunks.Add(newChunk);
    }
}