using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{   
    public Transform FruitPosition;
    public Transform ToolPosition;
    public ToolPropertiesDefinition toolProperties;
    
    public void ShowSelectedFruit(Fruit fruit)
    {
        foreach(Transform child in FruitPosition)
            Destroy(child.gameObject);
        Instantiate(fruit.mesh, FruitPosition);
    }

    public void ShowSelectedTool(Tool.ToolType tool)
    {
        foreach(Transform child in ToolPosition)
            Destroy(child.gameObject);
        ToolProperties toolProperty = toolProperties.tools.Find(property => property.tool == tool);
        Instantiate(toolProperty.mesh, ToolPosition);
    }
    
    public void Clear(){
        foreach(Transform child in FruitPosition)
            Destroy(child.gameObject);
        foreach(Transform child in ToolPosition)
            Destroy(child.gameObject);
    }
}
