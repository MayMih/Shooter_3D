using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// —овсем простенький  скрипт прицел:
/// </summary>
/// <seealso cref="https://3dgame-creator.ru/catalog/uroki/unity-5-skript-pricela/"/>
/// <remarks>
/// ¬ам просто нужно повесить скрипт на персонажа и указать в переменной <see cref="skin"/> вашу 2D текстуру прицела. 
/// ≈сли размеры отображени€ прицела покажутс€ вам слишком маленькими или слишком большими, измените размер с 40 
/// на более подход€щий вам.
/// </remarks>
public class CrossHair : MonoBehaviour 
{ 
    public Texture2D skin; 
    
    [SerializeField] private int size = 21;
    
    public void OnGUI() 
    { 
        GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, size, size), skin); 
    }


}
