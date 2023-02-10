using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BetaSelection : MonoBehaviour
{
  public Text selectedTool;
  public Text selectedFruit;

  private Dictionary<Tool.ToolType, string> toolsLabels = new Dictionary<Tool.ToolType, string>(){
    {Tool.ToolType.Hammer, "Hammer"},
    {Tool.ToolType.Star, "Star"},
    {Tool.ToolType.Eye, "Eye"},
    {Tool.ToolType.Element, "Element"},
};
  void Start()
  {
    Clear();
  }

  public void ShowSelectedTool(Tool.ToolType tool)
  {
    selectedTool.text = toolsLabels[tool];
  }

  public void ShowSelectedFruit(Fruit fruit)
  {
    selectedFruit.text = fruit.appleName;
  }

  public void Clear()
  {
    selectedTool.text = "";
    selectedFruit.text = "";
  }
}
