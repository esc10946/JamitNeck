using UnityEngine;

public enum TileType
{
    Normal   = 0,
    Danger   = 1,
    Spin     = 2,
    Ice      = 3,
    Trap     = 4,
    Electric = 5,
    Fog      = 6,
    Random   = 7,
    Destroyed = -1
}

public interface ITimeEvent
{
    void OnPlayerEnter(GameObject player);
}

public class TileComp : MonoBehaviour
{
    public Sprite[] tileSprite;
    public float randomTileTime= 5.0f;
    public float recoveryTileTime = 3.0f;

    Vector2 worldPosition;
    [SerializeField]
    TileType currentTileType = TileType.Normal;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //��Ҵٰ� ġ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsWalkable())
        {
            //�ϴ��� Ÿ�Ͽ��� �ݸ������� �÷��̾� �Ǵ��ϱ�
            Debug.Log(GetTileType().ToString());
            SetTileType(TileType.Danger);
        }
    }

    void ChangeRandomTile()
    {
        int tileIndex = Random.Range(0, tileSprite.Length - 1);
        SetTileType((TileType)tileIndex);
    }
    public bool IsWalkable()
    {
        return currentTileType != TileType.Danger && currentTileType != TileType.Destroyed;
    }
    public TileType GetTileType() { return currentTileType; }

    public void SetTileType(TileType intputTileType)
    {
        currentTileType = intputTileType;
        // �������� Ÿ�� �ٲٱ�
        if (currentTileType == TileType.Random) ChangeRandomTile();
        updateTileImage();
    }
    private void updateTileImage()
    {
        int tileIndex = (int)currentTileType;

        if (!spriteRenderer || !tileSprite[tileIndex]) return;
        spriteRenderer.sprite = tileSprite[tileIndex];
    }
    private void TileEvent()
    {
        switch (currentTileType)
        {
            case TileType.Danger:

                break;
            case TileType.Destroyed:
                Collider2D col = GetComponent<Collider2D>();
                col.isTrigger = false;
                break;
            default:
                break;
        }
    }
}
