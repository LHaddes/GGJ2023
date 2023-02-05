using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ToolProperties
{
  public Tool.ToolType tool;
  public GameObject mesh;
  public GameObject sprite;
  public AudioClip sound;
}

[CreateAssetMenu(menuName = "Tool/Properties Definition")]
public class ToolPropertiesDefinition : ScriptableObject
{
  public List<ToolProperties> tools;
}