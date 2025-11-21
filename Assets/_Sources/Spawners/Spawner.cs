using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private float _countObjectsInSpawn;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _countSpawnObjects;
    [SerializeField] private bool _globalSpawn;

    private ObjectPool<T> _objectPool;
    private Coroutine _spawnCoroutine;
    private bool _isInit;
    private bool _isActive;

    [field: SerializeField] public float MaxLifeTimeSeconds { get; private set; }
    [field: SerializeField] public float MinLifeTimeSeconds { get; private set; }

    public event Action<int> Spawn;
    public event Action<int> ChangeActiveObject;
    
    private void Update()
    {
        if (_isActive == false)
            return;
        
        if(_globalSpawn)
            if (_spawnCoroutine == null)
                _spawnCoroutine = StartCoroutine(SpawnWithDelay());
    }
    
    public void Init()
    {
        _objectPool = new ObjectPool<T>
        ( 
            createFunc : () => Create(GetRandomPosition()),
            actionOnGet : (spawnObject) => ActionOnGet(spawnObject),
            actionOnRelease : (spawnObject) => spawnObject.gameObject.SetActive(false),
            actionOnDestroy : (spawnObject) => Delete(spawnObject),
            collectionCheck : true,
            defaultCapacity : _poolCapacity,
            maxSize : _poolMaxSize
        );
        
        _isInit = true;
    }
    
    public void Activate()
    {
        if (_isInit == false)
        {
            Debug.LogError($"{nameof(Spawner<T>)} has not been init");
            return;
        }
        
        _isActive = true;
    }
    
    public void Deactivate()
    {
        _isActive = false;
    }

    public void GetObjectFromPool(Transform spawnTransform)
    {
        if (_isActive == false)
        {
            Debug.LogError($"{nameof(Spawner<T>)} has not been activate");
            return;
        }
        
        T objectFromPool = _objectPool.Get();
        objectFromPool.gameObject.transform.position = spawnTransform.position;
        objectFromPool.gameObject.transform.rotation = spawnTransform.rotation;
    }

    public virtual void ActionOnGet(T spawnObject)
    {
        spawnObject.gameObject.SetActive(true);
        ChangeActiveObject?.Invoke(_objectPool.CountActive);
    }

    public virtual T Create(Vector3 vector3)
    {
        if (_isActive == false)
            throw new ArgumentException($"{nameof(Spawner<T>)} not activate");
        
        T spawnObject = Instantiate(_prefab, vector3, transform.rotation);

        _countSpawnObjects++;
        Spawn?.Invoke(_countSpawnObjects);
        

        return spawnObject;
    }

    public virtual void Delete(T spawnObject)
    {
        float lifeTimeSeconds = Random.Range(MinLifeTimeSeconds, MaxLifeTimeSeconds);
        Destroy(spawnObject.gameObject, lifeTimeSeconds);
    }

    public virtual void ReleaseT(T spawnObject)
    {
        _objectPool.Release(spawnObject);
        ChangeActiveObject?.Invoke(_objectPool.CountActive);
    }

    protected Vector3 GetRandomPosition()
    {
        Bounds spawnArea = GetComponent<Collider>().bounds;

        float positionX = Random.Range(spawnArea.min.x, spawnArea.max.x);
        float positionY = Random.Range(spawnArea.min.y, spawnArea.max.y);
        float positionZ = Random.Range(spawnArea.min.z, spawnArea.max.z);

        return new Vector3(positionX, positionY, positionZ);
    }

    private IEnumerator SpawnWithDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        for (int i = 0; i < _countObjectsInSpawn; i++)
            _objectPool.Get();

        yield return wait;

        _spawnCoroutine = null;
    }
}