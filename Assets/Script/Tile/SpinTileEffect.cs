using UnityEngine;

public class SpinTileEffect : MonoBehaviour, ISpecialTile
{
    public void Activate(GameObject player)
    {
        Debug.Log("SpinTileEffect �ߵ�");
        // ���� ����
        var move = player.GetComponent <PlayerController >();
        if (move != null)
        {
            //move.InvertInput(); // �¿� ���� ���� �ؾ���
        }
    }

    public void ResetTile() { }
}
