using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
  private Tool.ToolType? selectedTool = null;
  private Fruit selectedFruit = null;
  private bool locked = true;
  private bool success = false;
  private bool started = false;

  public UnityEvent onExitStartScreen = new UnityEvent();
  public UnityEvent<Tool.ToolType> onSelectTool = new UnityEvent<Tool.ToolType>();
  public UnityEvent<Fruit> onSelectFruit = new UnityEvent<Fruit>();
  public UnityEvent onClearSelection = new UnityEvent();
  public UnityEvent<bool> onFusionClickable = new UnityEvent<bool>();
  public UnityEvent<Tool.ToolType, Fruit> onClickFuse = new UnityEvent<Tool.ToolType, Fruit>();

  public GameInventory inventory;
  public List<ToolButton> toolButtons;


  public TMP_Text notifications;
  public GameObject victoryMessage;
  public GameObject startScreen;
  public GameObject[] gameUI;

  public GameObject fruitButtonPrefab;
  public RectTransform fruitsPanel;

  public List<string> failMessages;

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


    if (victoryMessage.activeInHierarchy)
      victoryMessage.SetActive(false);
    notifications.CrossFadeAlpha(0f, 0.1f, true);

    notifications.text = "";

    foreach (GameObject child in gameUI)
    {
      child.SetActive(false);
    }
  }

  void Update()
  {
    if (!started && Input.GetMouseButtonDown(0))
    {
      startScreen.SetActive(false);
      foreach (GameObject child in gameUI)
      {
        child.SetActive(true);
      }

      onExitStartScreen.Invoke();
      started = true;
      locked = false;
    }
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
    notifications.CrossFadeAlpha(1f, 0.4f, true);
    notifications.text = success ? fruit.name : failMessages[Random.Range(0, failMessages.Count)];

    yield return new WaitForSeconds(2f);
    notifications.CrossFadeAlpha(0f, 0.4f, true);
  }

  public void DisplayResult(bool res)
  {
    success = res;
  }

  public void DisplayVictory()
  {
    victoryMessage.SetActive(true);
    StartCoroutine(RemoveVictoryMessage());
  }

  IEnumerator RemoveVictoryMessage()
  {
    yield return new WaitForSeconds(2f);
    victoryMessage.SetActive(false);
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

  public void BeginGame()
  {
    GetComponent<CanvasGroup>().alpha = 1f;
    locked = false;
  }
}