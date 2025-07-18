using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //언젠간 변수 프로퍼티로 보호해주기
    public GameObject tilePrefab;
    public int width = 5, height = 5; //일단 프로토타입에서는 5*5 
    public Vector2 spawnTilePoint; // 스폰을 시작할 좌표
    [SerializeField] private GameObject fogEffectPrefab;


    TileComp[,] tiles;
    public List<TileType> roundTileTypes = new List<TileType>();

    void Start()
    {
        //게임 시작시 타일 생성
        GeneratedGrid();
    }

    void GeneratedGrid()
    {
        tiles = new TileComp[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tileObj = Instantiate(tilePrefab, new Vector3(x + spawnTilePoint.x, y + spawnTilePoint.y, y), Quaternion.identity, this.transform);
                TileComp tileComp = tileObj.GetComponent<TileComp>();
                tileComp.nodePosition = new Vector2Int(x, y);
                tileComp.tileManager = this;

                // 외곽 타일은 충돌 활성화
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    tileObj.GetComponent<Collider2D>().isTrigger = false; // 충돌 ON
                    tileComp.isOuterWall = true;
                    tileObj.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    tileComp.SetTileType(TileType.Edge);
                }

                tiles[x, y] = tileComp;
            }
        }
    }

    TileComp GetTile(Vector2Int coord)
    {
        if(1 < coord.x && coord.x < width - 1 && 1 < coord.y && coord.y < height - 1)
        {
            return tiles[coord.x,coord.y];
        }
        return null;
    }

    public void SpawnSpecialTile(TileType type)
    {
        List<TileComp> normalTiles = GetNormalTiles();
        if (normalTiles.Count == 0) return;

        TileComp randomTile = normalTiles[Random.Range(0, normalTiles.Count)];
        randomTile.SetTileType(type);
    }

    public List<TileComp> GetNormalTiles()
    {
        return tiles.Cast<TileComp>().Where(t => t.GetTileType() == TileType.Normal).ToList();
    }

    public List<TileComp> GetEdgeTiles()
    {
        List<TileComp> edgeTiles = new List<TileComp>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    edgeTiles.Add(tiles[x, y]);
                }
            }
        }
        return edgeTiles;
    }
    public void SpawnFogEffectOnRandomTile()
    {
        List<TileComp> allTiles = tiles.Cast<TileComp>().ToList();
        if (allTiles.Count == 0) return;

        TileComp target = allTiles[Random.Range(0, allTiles.Count)];

        if (fogEffectPrefab != null)
        {
            GameObject fog = Instantiate(fogEffectPrefab, target.transform.position, Quaternion.identity, this.transform);
            Destroy(fog, 3f);
        }
    }

    public void ResetTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileComp tile = tiles[x, y];

                foreach (var effect in tile.GetComponents<ISpecialTile>())
                {
                    effect.ResetTile();
                }

                tile.StopAllCoroutines();
            }
        }
    }

    public Vector3 GetLastTilePositionInDirection(Vector3 start, Vector3 dir)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(start, dir, 100f, LayerMask.GetMask("Tile"));
        if (hits.Length == 0) return start;

        return hits[hits.Length - 1].point;
    }
}
