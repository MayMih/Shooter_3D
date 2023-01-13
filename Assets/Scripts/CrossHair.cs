using UnityEngine;
using UnityEngine.UI;

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
    
    public void OnGUI() 
    { 
        GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, size, size), skin); 
    }


}