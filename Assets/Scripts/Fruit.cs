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
      Basique,
      Commune,
      Rare,
      Epique,
      Légendaire
    }

    public Rarity rarity;
    public Sprite sprite;
    public GameObject mesh;
    public AudioClip sound;
    public bool isUnlocked;
    public bool isUnlockedOnStart;
    public List<Recipe> recipeList = new List<Recipe>();

    public void OnEnable()
    {
        if (name != "Pomme")
            isUnlocked = false;
    }

    public void OnDisable()
    {
        if (name != "Pomme")
            isUnlocked = false;
    }
}

[Serializable]
public class Recipe
{
    public Fruit fruit;
    public Tool.ToolType tool;
}