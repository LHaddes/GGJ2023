using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Fruit/New Inventory")]
public class GameInventory : ScriptableObject
{
    public List<Fruit> allFruits = new List<Fruit>();
    public Fruit failureFruit;

    public UnityAction<Fruit> onFruitObtained;
    public UnityAction<List<Fruit>> onFruitsAvailableUpdated;
    public UnityAction onAllFruitsFound;

    [HideInInspector] public UnityEvent unlockAppleInEncyclopedia = new UnityEvent();


    public void StartGame()
    {
        UIManager.Instance.AddFruitButton(allFruits[0]);
    }

    public void ObtainFruit(Fruit fruit)
    {
        onFruitObtained.Invoke(fruit);
        if (!fruit.isUnlocked)
        {
            fruit.isUnlocked = true;
            var availableFruits = ListAvailableFruits();
            unlockAppleInEncyclopedia.Invoke();
            UIManager.Instance.AddFruitButton(fruit);

            if (availableFruits.Count == allFruits.Count)
            {
                onAllFruitsFound.Invoke();
            }
        }
    }

    public List<Fruit> ListAvailableFruits()
    {
        return allFruits.FindAll(fruit => fruit.isUnlocked);
    }

    public Fruit GetFusion(Tool.ToolType tool, Fruit fruit)
    {
        foreach (Fruit resultFruit in allFruits)
        {
            foreach (Recipe recipe in resultFruit.recipeList)
            {
                if (recipe.fruit == fruit && recipe.tool == tool)
                {
                    return resultFruit;
                }
            }
        }
        
        return failureFruit;
    }
}