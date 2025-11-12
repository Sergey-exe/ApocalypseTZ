using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private BonusesSpawner _bonusesSpawner;
    
    public override Enemy Create(Vector3 vector3)
    {
        Enemy enemy = base.Create(vector3);
        
        enemy.IsKilled += ReleaseT;
        enemy.OnDeathSpawnPoint += _bonusesSpawner.SpawnBonus;

        if (!enemy.TryGetComponent(out Health health))
            throw new ArgumentNullException($"На враге отсутствует необходимый компонент {nameof(Health)}!");

        if (!enemy.TryGetComponent(out PlayerDetector playerDetector))
            throw new ArgumentNullException(
                $"На враге отсутствует необходимый компонент для обнаружения игрока по близости {nameof(PlayerDetector)}!");

        if (!enemy.TryGetComponent(out EnemyMover mover))
            throw new ArgumentNullException(
                $"На враге осутствует скрипт необходимый для передвижения {nameof(EnemyMover)}!");

        if (!enemy.TryGetComponent(out EnemyAttacker attacker))
            throw new ArgumentNullException(
                $"На враге осутствует скрипт необходимый для атаки {nameof(EnemyAttacker)}!");
        
        if (!enemy.TryGetComponent(out UnitAnimator animator))
            throw new ArgumentNullException($"На враге осутствует скрипт необходимый для корректного проигрывания анимации {nameof(UnitAnimator)}!");
        
        if(!enemy.TryGetComponent(out DeathHandler deathHandler))
            throw new ArgumentNullException($"На враге осутствует скрипт необходимый для корректного осуществления смерти {nameof(DeathHandler)}!");
        
        health.Init();
        playerDetector.Init();
        mover.Init();
        attacker.Init();
        animator.Init();
        deathHandler.Init();
        
        health.Activate();
        deathHandler.Activate();
        playerDetector.Activate();
        mover.Activate();
        animator.Activate();
        attacker.Activate();
        
        return enemy;
    }



    public override void Delete(Enemy enemy)
    {
        enemy.IsKilled -= ReleaseT;
        enemy.OnDeathSpawnPoint -= _bonusesSpawner.SpawnBonus;
        
        base.Delete(enemy);
    }
}
