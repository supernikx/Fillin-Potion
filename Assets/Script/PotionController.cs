using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour, IPotion
{

    public float speed;

    public PotionState _currentState;
    public PotionState CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
        }
    }

    public PotionTypes _currentType;
    public PotionTypes CurrentType
    {
        get
        {
            return _currentType;
        }

        set
        {
            _currentType = value;
        }
    }

    public PotionTypes _refillerZone;
    public PotionTypes RefillerZone
    {
        get
        {
            return _refillerZone;
        }
        set
        {
            _refillerZone = value;
        }
    }

    public bool _inRefillerZone;
    public bool InRefillerZone
    {
        get
        {
            return _inRefillerZone;
        }
        set
        {
            _inRefillerZone = value;
        }
    }

    public bool refill;
    public bool wrong;

    MeshRenderer mr;
    bool gameover;

    // Use this for initialization
    void Start()
    {
        refill = false;
        mr = GetComponent<MeshRenderer>();
        EventManager.GameOver += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            if (CurrentState == PotionState.inGame)
            {
                Movement();
            }
        }
    }

    private void Movement()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ScreenLimit")
        {
            if (EventManager.OnPotionDestroy != null)
            {
                EventManager.OnPotionDestroy(this);
            }
        }

        if (other.gameObject.tag == "Refiller")
        {
            RefillerZone = other.gameObject.GetComponent<RefilerController>().potionRefill;
            InRefillerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Refiller")
        {
            InRefillerZone = false;
        }
    }

    public void Spawn(Vector3 spawnPosition, PotionTypes type, float _speed)
    {
        refill = false;
        speed = _speed;
        transform.position = spawnPosition;
        CurrentType = type;
        SetMaterial();
        CurrentState = PotionState.inGame;
        if (EventManager.OnPotionSpawn != null)
            EventManager.OnPotionSpawn(this);
    }

    public void Refill()
    {
        refill = true;
        mr.material.color = Color.white;
    }

    public bool isRefilled()
    {
        return refill;
    }

    public void WrongLiquid()
    {
        mr.material.color = Color.black;
        wrong = true;
    }

    public bool isWrong()
    {
        return wrong;
    }

    void GameOver()
    {
        gameover = true;
    }

    //funzione molto provvisoria
    private void SetMaterial()
    {
        switch (CurrentType)
        {
            case PotionTypes.Q:
                mr.material.color = Color.red;
                break;
            case PotionTypes.R:
                mr.material.color = Color.green;
                break;
            case PotionTypes.U:
                mr.material.color = Color.yellow;
                break;
            case PotionTypes.P:
                mr.material.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
