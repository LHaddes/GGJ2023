using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
  private Tool.ToolType? selectedTool = null;
  private Fruit selectedFruit = null;
  private bool locked = false;

  public UnityEvent<Tool.ToolType> onSelectTool = new UnityEvent<Tool.ToolType>();
  public UnityEvent<Fruit> onSelectFruit = new UnityEvent<Fruit>();
  public UnityEvent onClearSelection = new UnityEvent();
  public UnityEvent<bool> onFusionClickable = new UnityEvent<bool>();
  public UnityEvent<Tool.ToolType, Fruit> onClickFuse = new UnityEvent<Tool.ToolType, Fruit>();

  public GameInventory inventory;
  public List<ToolButton> toolButtons;

  public Text notifications;
  public GameObject victoryScreen;

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
    inventory.onFruitObtained += DisplayFruitObtained;
    inventory.onAllFruitsFound += DisplayVictory;

    victoryScreen.SetActive(false);
    notifications.text = "";
  }

  void SelectTool(Tool.ToolType tool)
  {
    if (locked) return;

    selectedTool = tool;
    onSelectTool.Invoke(tool);

    if (selectedFruit != null)
    {
      onFusionClickable.Invoke(true);
    }
  }

  void SelectFruit(Fruit fruit)
  {
    if (locked) return;

    selectedFruit = fruit;
    onSelectFruit.Invoke(fruit);

    if (selectedTool != null)
    {
      onFusionClickable.Invoke(true);
    }
  }

  public void ClickFuse()
  {
    if (selectedTool != null && selectedFruit != null)
    {
      onClickFuse.Invoke(selectedTool.GetValueOrDefault(), selectedFruit);
      locked = true;
    }
  }

  void FillFruitsPanel(List<Fruit> fruits)
  {
    // Clear existing buttons
    foreach (Transform btn in fruitsPanel)
    {
      Destroy(btn.gameObject);
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

  public void DisplayFruitObtained(Fruit fruit)
  {
    ResetSelection();
    StartCoroutine(NotifyFruit(fruit));
  }

  IEnumerator NotifyFruit(Fruit fruit)
  {
    notifications.text = "You obtained a " + fruit.name + "!";
    yield return new WaitForSeconds(2f);
    notifications.text = "";
  }

  public void DisplayFail()
  {
    notifications.text = "Nothing happens…";
    ResetSelection();
  }

  public void DisplayVictory()
  {
    victoryScreen.SetActive(true);
  }

  void ResetSelection()
  {
    selectedTool = null;
    selectedFruit = null;
    onFusionClickable.Invoke(false);
    onClearSelection.Invoke();
  }

  public void Unlock()
  {
    locked = false;
  }
}