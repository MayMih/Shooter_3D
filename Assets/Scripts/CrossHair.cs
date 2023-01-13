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
    
    public void OnGUI() 
    { 
        GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, 40, 40), skin); 
    } 
}
