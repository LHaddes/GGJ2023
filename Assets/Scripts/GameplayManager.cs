using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
  public Fruit fruitToUse;
  public Tool.ToolType toolToUse;

  public List<Fruit> allFruits = new List<Fruit>();

  public GameInventory inventory;

  // Start is called before the first frame update
  void Start()
  {
    inventory.StartGame();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Fuse()
  {
    foreach (Fruit f in allFruits)
    {
      foreach (Recipe r in f.recipeList)
      {
        if (r.fruit == fruitToUse && r.tool == toolToUse)
        {
          Fruit resultFruit = f;
          Debug.Log($"You just crafted {resultFruit.name}");

          return;
        }
      }
    }

    Debug.Log("Combinaison does not work");
  }
}
