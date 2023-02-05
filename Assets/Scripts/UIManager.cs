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

    public UnityEvent<Tool.ToolType> onSelectTool = new UnityEvent<Tool.ToolType>();
    public UnityEvent<Fruit> onSelectFruit = new UnityEvent<Fruit>();
    public UnityEvent onClearSelection = new UnityEvent();
    public UnityEvent<Tool.ToolType, Fruit> onClickFuse = new UnityEvent<Tool.ToolType, Fruit>();

    public GameInventory inventory;
    public Button fuseButton;
    public List<ToolButton> toolButtons;

    public TMP_Text notifications;
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

        fuseButton.interactable = false;
        fuseButton.onClick.AddListener(FuseSelected);

        if(victoryScreen.activeInHierarchy)
            victoryScreen.SetActive(false);
        
        notifications.text = "";
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

    void FuseSelected()
    {
        if (selectedTool != null && selectedFruit != null)
        {
            onClickFuse.Invoke(selectedTool.GetValueOrDefault(), selectedFruit);
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
        notifications.text = "You obtained a " + fruit.name + "!";
        ResetSelection();
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
        fuseButton.interactable = false;
        onClearSelection.Invoke();
    }
}