using UnityEngine;

public class BonusesSpawner : MonoBehaviour
{
    [Range(0f, 100f)]
    [SerializeField] private float _spawnСhance;
    [SerializeField] private Spawner<Bonus>[] _bonuses;

    public void SpawnBonus(Transform spawnPoint)
    {
        float randomNumber;
        
        for (int i = 0; i < _bonuses.Length; i++)
        {
            randomNumber = Random.Range(0f, 100f);
            
            if (randomNumber <= _spawnСhance)
                _bonuses[i].GetObjectFromPool(spawnPoint);
        }
    }
}
