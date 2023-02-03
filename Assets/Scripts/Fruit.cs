using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fruit/New Fruit")]
public class Fruit : ScriptableObject
{
    public string name;
    public Sprite img;
    public List<Recipe> recipeList = new List<Recipe>();
}

[Serializable]
public class Recipe
{
    public Fruit fruit;
    public Tool.ToolType tool;
}