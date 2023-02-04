using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public Fruit fruitToUse;
    public Tool.ToolType toolToUse;

    public List<Fruit> allFruits = new List<Fruit>();
    
  public GameInventory inventory;

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

    public void Fuse()
    {
        //On vérifie tous les fruits du jeu
        foreach (Fruit f in allFruits)
        {
            //Pour chaque fruit, on vérifie les différentes recettes possibles
            foreach (Recipe r in f.recipeList)
            {
                if (r.fruit == fruitToUse && r.tool == toolToUse)
                {
                    Fruit resultFruit = f;  //Si la recette réalisée correspond à une recette exitante, on donne le résultat de la recette au joueur
                    Debug.Log($"You just crafted {resultFruit.name}");
                    
                    return;
                }
            }
        }
        
        //Sinon, la recette ne donne rien et on perd le fruit utilisé
        Debug.Log("Combinaison does not work");
    }
}
