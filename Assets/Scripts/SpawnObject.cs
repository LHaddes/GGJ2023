using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{   
    public Transform FruitPosition;
    public Transform ToolPosition;
    
    public void ShowSelectedFruit(Fruit fruit)
    {
        foreach(Transform child in FruitPosition)
            Destroy(child.gameObject);
        Instantiate(fruit.mesh, FruitPosition);
    }

    // public void ShowSelectedTool(Tool tool)
    // {
    //     foreach(Transform child in ToolPosition)
    //         Destroy(child.gameObject);
    //     Instantiate(tool.mesh, ToolPosition);
    // }
    
    public void Clear(){
        foreach(Transform child in FruitPosition)
            Destroy(child.gameObject);
        foreach(Transform child in ToolPosition)
            Destroy(child.gameObject);
    }
}
