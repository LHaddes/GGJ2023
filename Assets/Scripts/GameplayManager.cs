using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
  public GameInventory inventory;

  public UnityEvent onFail = new UnityEvent();

    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
   // inventory.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fuse(Tool.ToolType tool, Fruit fruit)
    {
        Fruit newFruit = inventory.GetFusion(tool, fruit);
        if (newFruit != null)
        {
            inventory.ObtainFruit(newFruit);
        }
        else
        {
            onFail.Invoke();
        }
    }
}
