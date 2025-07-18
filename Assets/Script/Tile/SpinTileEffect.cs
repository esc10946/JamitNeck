using UnityEngine;

public class SpinTileEffect : MonoBehaviour, ISpecialTile
{
    public void Activate(GameObject player)
    {
        Debug.Log("SpinTileEffect �ߵ�");

        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.InvertInput(2f); // 2�ʰ� �¿� ����
    }


    public void ResetTile() { }
}
