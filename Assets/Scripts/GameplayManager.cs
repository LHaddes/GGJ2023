using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
  public static GameplayManager Instance;
  public GameInventory inventory;

  public UnityEvent<bool> onFuse = new UnityEvent<bool>();
  public UnityEvent onEndTurn = new UnityEvent();

  private bool started = false;

  public GameObject pauseMenu;
  private bool _isPaused;

  void Awake()
  {
    Instance = this;
  }

  // Update is called once per frame
  void Update()
  {
    // TODO: Start menu
    if (!started)
    {
      started = true;
      inventory.StartGame();
    }

    if (Input.GetKeyDown(KeyCode.A))
    {
      inventory.ObtainFruit(inventory.allFruits[1]);
    }
  }

  public void TogglePauseMenu()
  {
    if (_isPaused)
    {
      Time.timeScale = 1f;
      pauseMenu.SetActive(false);
      _isPaused = false;
    }
    else
    {
      Time.timeScale = 0f;
      pauseMenu.SetActive(true);
      _isPaused = true;
    }
  }

  public void Quit()
  {
    Application.Quit();
  }


  public void Fuse(Tool.ToolType tool, Fruit fruit)
  {
    Fruit newFruit = inventory.GetFusion(tool, fruit);
    onFuse.Invoke(newFruit != inventory.defaultFruit);
    inventory.ObtainFruit(newFruit);

    StartCoroutine(EndTurn());
  }

  IEnumerator EndTurn()
  {
    yield return new WaitForSeconds(2f);
    onEndTurn.Invoke();
  }
}
