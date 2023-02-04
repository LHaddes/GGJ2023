using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
  private Tool.ToolType? selectedTool = null;
  private Fruit selectedFruit = null;

  public UnityEvent<Tool.ToolType> onSelectTool = new UnityEvent<Tool.ToolType>();
  public UnityEvent<Fruit> onSelectFruit = new UnityEvent<Fruit>();
  public UnityEvent onClearSelection = new UnityEvent();

  public GameInventory inventory;
  public Button fuseButton;
  public List<ToolButton> toolButtons;

  // Start is called before the first frame update
  void Start()
  {
    // Connect tools buttons to event
    foreach (ToolButton btn in toolButtons)
    {
      btn.onSelect += SelectTool;
    }
  }

  void SelectTool(Tool.ToolType tool)
  {
    selectedTool = tool;
    onSelectTool.Invoke(tool);

    if (selectedFruit != null)
    {
      fuseButton.interactable = true;
    }
  }
  void SelectFruit(Fruit fruit)
  {
    selectedFruit = fruit;
    onSelectFruit.Invoke(fruit);

    if (selectedTool != null)
    {
      fuseButton.interactable = true;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
