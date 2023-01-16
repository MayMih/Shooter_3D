using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Совсем простенький  скрипт прицел:
/// </summary>
/// <seealso cref="https://3dgame-creator.ru/catalog/uroki/unity-5-skript-pricela/"/>
/// <remarks>
/// Вам просто нужно повесить скрипт на персонажа и указать в переменной <see cref="skin"/> вашу 2D текстуру прицела. 
/// Если размеры отображения прицела покажутся вам слишком маленькими или слишком большими, измените размер с 40 
/// на более подходящий вам.
/// </remarks>
public class CrossHair : MonoBehaviour 
{ 
    public Texture2D skin; 
    
    [SerializeField] private int size = 21;
    [SerializeField] private Color colour = Color.white;

    private List<Color> knownColors = new List<Color>() { 
        Color.blue, Color.green, Color.red, Color.cyan, Color.magenta, Color.white, Color.yellow 
    };
    private int crossIndex;


    private void Awake()
    {
        if (!knownColors.Contains(colour))
        {
            knownColors.Add(colour);
        }
        crossIndex = knownColors.Count - 1;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Метод отрисовки прицела с контрастной обводкой
    /// </summary>
    public void OnGUI() 
    {
        var rect = new Rect(Screen.width / 2 + 1, Screen.height / 2 + 1, size, size);
        GUI.DrawTexture(rect, skin, ScaleMode.ScaleToFit, true, 0, Color.black, 0, 0);
        rect.x--;
        rect.y--;
        GUI.DrawTexture(rect, skin, ScaleMode.ScaleToFit, true, 0, colour, 0, 0);        
    }

    internal void ChangeColor()
    {
        //Debug.Log($"Index Before: {crossIndex}");
        crossIndex = crossIndex >= knownColors.Count - 1 ? 0 : crossIndex + 1;
        //Debug.Log($"Index After: {crossIndex}");
        colour = knownColors[crossIndex];
    }
}
