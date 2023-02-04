using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToolButton : MonoBehaviour
{
  public Tool.ToolType tool;

  public UnityAction<Tool.ToolType> onSelect;

  void Start()
  {
    GetComponent<Button>().onClick.AddListener(SelectTool);
  }

  void SelectTool()
  {
    onSelect.Invoke(tool);
  }
}
