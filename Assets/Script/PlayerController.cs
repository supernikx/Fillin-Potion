using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UIManager ui;
    public List<IPotion> inGamePotions = new List<IPotion>();
    public PotionTypes keypressed;
    public int score;

    // Use this for initialization
    void Start()
    {
        EventManager.OnPotionSpawn += PotionSpawned;
        EventManager.OnPotionDestroy += PotionDestroyed;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keypressed = PotionTypes.Q;
            CheckFill();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            keypressed = PotionTypes.R;
            CheckFill();
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            keypressed = PotionTypes.U;
            CheckFill();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            keypressed = PotionTypes.P;
            CheckFill();
        }
    }

    public void CheckFill()
    {
        foreach (IPotion potion in inGamePotions)
        {
            if (keypressed == potion.CurrentType && keypressed == potion.RefillerZone && potion.InRefillerZone && !potion.isRefilled() && !potion.isWrong())
            {
                potion.Refill();
            }
            else if (keypressed != potion.CurrentType && keypressed == potion.RefillerZone && potion.InRefillerZone && !potion.isRefilled() && !potion.isWrong())
            {
                potion.WrongLiquid();
            }
            else
            {
                StartCoroutine(ui.WrongKeyText());
            }
        }
    }

    public void PotionSpawned(IPotion _potion)
    {
        inGamePotions.Add(_potion);
    }

    public void PotionDestroyed(IPotion _potion)
    {
        inGamePotions.Remove(_potion);
        if (_potion.isRefilled())
        {
            score += 100;
            ui.ScoreUpdate(score);
        }
        else if (_potion.isWrong())
            EventManager.GameOver();
    }
}
