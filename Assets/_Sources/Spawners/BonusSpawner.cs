using UnityEngine;

public class BonusSpawner : Spawner<Bonus>
{
    public override Bonus Create(Vector3 transform)
    {
        Bonus bonus = base.Create(GetRandomPosition());
        bonus.Upped += ReleaseT;

        return bonus;
    }

    public override void Delete(Bonus bonus)
    {
        bonus.Upped -= ReleaseT;

        base.Delete(bonus);
    }
}