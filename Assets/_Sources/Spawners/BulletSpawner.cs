using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArgumentNullException = System.ArgumentNullException;

public class BulletSpawner : Spawner<Bullet>
{
    public override Bullet Create(Vector3 vector3)
    {
        Bullet bullet = base.Create(vector3);

        if (!bullet.TryGetComponent(out BulletMover bulletMover))
            throw new ArgumentNullException($"На пуле отсутствует класс {nameof(BulletMover)} необходимый для движения пули!");

        bullet.Hit += ReleaseT;
        bulletMover.Activate();
        
        return bullet;
    }

    public override void Delete(Bullet bullet)
    {
        bullet.Hit -= ReleaseT;
        
        base.Delete(bullet);
    }
}
