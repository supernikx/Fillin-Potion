using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionState
{
    inGame,
    inPool,
}

public class PoolManager : MonoBehaviour
{

    public GameObject potionPrefab;
    public int ammount;
    public List<IPotion> pooledPotions = new List<IPotion>();
    public static PoolManager instance;
    Vector3 poolPosition = new Vector3(1000, 1000, 1000);

    private void Awake()
    {
        instance = this;
    }


    // Use this for initialization
    void Start()
    {
        EventManager.OnPotionDestroy += OnPotionDestroyed;
        for (int i = 0; i < ammount; i++)
        {
            IPotion instantiatedPotion = Instantiate(potionPrefab, transform).GetComponent<IPotion>();
            instantiatedPotion.gameObject.transform.position = poolPosition;
            instantiatedPotion.CurrentState = PotionState.inPool;
            pooledPotions.Add(instantiatedPotion);
        }
    }

    public IPotion GetPooledPotion()
    {
        foreach (IPotion _potion in pooledPotions)
        {
            if (_potion.CurrentState == PotionState.inPool)
                return _potion;
        }
        Debug.Log("non ci sono pozioni nella pool");
        return null;
    }

    public void OnPotionDestroyed(IPotion _potion)
    {
        _potion.CurrentState = PotionState.inPool;
        _potion.gameObject.transform.position = poolPosition;
    }

    private void OnDisable()
    {
        EventManager.OnPotionDestroy -= OnPotionDestroyed;
    }
}
