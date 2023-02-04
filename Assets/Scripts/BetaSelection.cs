using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BetaSelection : MonoBehaviour
{
  public Text selectedTool;
  public Text selectedFruit;

  private Dictionary<Tool.ToolType, string> toolsLabels = new Dictionary<Tool.ToolType, string>(){
    {Tool.ToolType.Tool1, "Hammer"},
    {Tool.ToolType.Tool2, "Star"},
    {Tool.ToolType.Tool3, "Eye"},
    {Tool.ToolType.Tool4, "Element"},
};
  void Start()
  {
    Clear();
  }

  public void ShowSelectedTool(Tool.ToolType tool)
  {
    selectedTool.text = toolsLabels[tool];
  }

  void ShowSelectedFruit(Fruit fruit)
  {
    selectedFruit.text = fruit.name;
  }

  void Clear()
  {
    selectedTool.text = "";
    selectedFruit.text = "";
  }
}
