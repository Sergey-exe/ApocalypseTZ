using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private UnitAnimator _playerAnimator;
    [SerializeField] private PlayerShooter _playerShooter;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BonusSpawner[] _bonusSpawners;
    
    private void Start()
    {
        foreach (var spawner in _bonusSpawners)
            spawner.Init();
        
        _bulletSpawner.Init();
        _enemySpawner.Init();
        _playerHealth.Init();
        _playerShooter.Init();
        _playerAnimator.Init();
        _inventory.Init();
        
        _playerHealth.Activate();
        _playerMover.Activate();
        _playerAnimator.Activate();
        _inventory.Activate();
        _bulletSpawner.Activate();
        _enemySpawner.Activate();
        _playerShooter.Activate();
        
        foreach (var spawner in _bonusSpawners)
            spawner.Activate();
    }
}