using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _shotSpeed;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Inventory _inventory;
    
    private Coroutine _shotCoroutine;

    public void Shot()
    {
        if(_shotCoroutine != null)
            return;
        
        if(!_inventory.TryGetItem(ItemType.CartridgeBox))
            return;

        _shotCoroutine = StartCoroutine(ShootCooldown());
        _bulletSpawner.GetObjectFromPool(_bulletSpawnPoint);
    }
    
    private IEnumerator ShootCooldown()
    {
        float delay = 1f / _shotSpeed;
        
        yield return new WaitForSeconds(delay);
        
        _shotCoroutine = null;
    }
}
