using System.Collections;
using UnityEngine;

public class SpinTileEffect : MonoBehaviour, ISpecialTile
{
    private bool triggered = false;
    private TileComp tile;

    private void Awake()
    {
        tile = GetComponent<TileComp>();
    }

    public void Activate(GameObject player)
    {
        if (triggered) return;
        triggered = true;

        Debug.Log("SpinTileEffect �ߵ�");

        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.InvertInput(2f); // 2�ʰ� �¿� ����

        StartCoroutine(tile.RevertTile());
    }

    public void ResetTile()
    {
        triggered = false;
        StopAllCoroutines();
    }
}
