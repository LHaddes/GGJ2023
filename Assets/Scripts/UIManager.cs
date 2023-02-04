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

  public GameObject fruitButtonPrefab;
  public RectTransform fruitsPanel;

  // Start is called before the first frame update
  void Start()
  {
    // Connect tools buttons to event
    foreach (ToolButton btn in toolButtons)
    {
      btn.onSelect += SelectTool;
    }

    inventory.onFruitsAvailableUpdated += FillFruitsPanel;
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

  void FillFruitsPanel(List<Fruit> fruits)
  {
    // Clear existing buttons
    foreach (GameObject btn in fruitsPanel)
    {
      Destroy(btn);
    }

    // Add new buttons
    foreach (Fruit fruit in fruits)
    {
      GameObject button = Instantiate(fruitButtonPrefab, fruitsPanel);
      FruitButton fruitBtn = button.GetComponent<FruitButton>();
      fruitBtn.fruit = fruit;
      fruitBtn.onSelect += SelectFruit;
    }
  }
}
