using System;

using UnityEngine;

/// <summary>
/// ������ �����������  ������ ������:
/// </summary>
/// <seealso cref="https://3dgame-creator.ru/catalog/uroki/unity-5-skript-pricela/"/>
/// <remarks>
/// ��� ������ ����� �������� ������ �� ��������� � ������� � ���������� <see cref="skin"/> ���� 2D �������� �������. 
/// ���� ������� ����������� ������� ��������� ��� ������� ���������� ��� ������� ��������, �������� ������ � 40 
/// �� ����� ���������� ���.
/// </remarks>
public class CrossHair : MonoBehaviour 
{ 
    public Texture2D skin; 
    
    [SerializeField] private int size = 21;
    [SerializeField] private Color colour = Color.white;

    private Color[] knownColors = { Color.blue, Color.gray, Color.green, Color.red, Color.cyan, Color.magenta, Color.white, Color.yellow };

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// ����� ��������� ������� � ����������� ��������
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
        colour = knownColors[UnityEngine.Random.Range(0, knownColors.Length)];
    }
}
