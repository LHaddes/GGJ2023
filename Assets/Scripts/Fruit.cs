using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Fruit/New Fruit")]
public class Fruit : ScriptableObject
{
    public string name;
    public string description;
    [Serializable]
    public enum Rarity
    {
      Base,
      Common,
      Rare,
      Epic,
      Legendary
    }

    public Rarity rarity;
    public Sprite sprite;
    public GameObject mesh;
    public bool isUnlocked;
    public bool isUnlockedOnStart;
    public List<Recipe> recipeList = new List<Recipe>();
}

[Serializable]
public class Recipe
{
    public Fruit fruit;
    public Tool.ToolType tool;
}