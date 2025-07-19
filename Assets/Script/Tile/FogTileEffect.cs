using UnityEngine;
using System.Collections;

public class FogTileEffect : MonoBehaviour, ISpecialTile
{
    private float timeSinceStepped = 0f;
    private bool triggered = false;
    private TileComp tile;

    [SerializeField] private float fogTriggerTime = 5f;

    private void Awake()
    {
        tile = GetComponent<TileComp>();
    }

    private void OnEnable()
    {
        timeSinceStepped = 0f;
        triggered = false;
    }

    private void Update()
    {
        if (triggered || tile.GetTileType() != TileType.Fog) return;

        timeSinceStepped += Time.deltaTime;

        if (timeSinceStepped >= fogTriggerTime)
        {
            triggered = true;
            Debug.Log("FogTileEffect �ߵ�");

            tile.tileManager.SpawnFogEffectOnRandomTile();

            tile.SetTileType(TileType.Normal);
        }
    }

    public void Activate(GameObject player)
    {
        timeSinceStepped = 0f; // �÷��̾ ������ �ʱ�ȭ
    }

    public void ResetTile()
    {
        timeSinceStepped = 0f;
        triggered = false;
        StopAllCoroutines();
    }
}
