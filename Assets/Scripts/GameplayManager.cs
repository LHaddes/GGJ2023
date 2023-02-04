using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
  public static GameplayManager Instance;
  public GameInventory inventory;

  public UnityEvent onFail = new UnityEvent();

  private bool started = false;

  void Awake()
  {
    Instance = this;
  }

  // Update is called once per frame
  void Update()
  {
      // TODO: Start menu
      if (!started)
      {
        started = true;
        inventory.StartGame();
    
      }
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
