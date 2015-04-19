using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum TileType
{
    wall,
    corridor,
    room,
    door,
    candidate
}

public struct Tile
{
    public TileType type;
    public int group;
    public Tile(TileType tt)
    {
        type = tt;
        group = 0;
    }
    public Tile(TileType tt, int grp)
    {
        type = tt;
        group = grp;
    }
}

public struct Bridge
{
    public int x;
    public int y;
    public int groupA;
    public int groupB;
}

public class GenerateLevel : MonoBehaviour {

    public GameObject wall;
    public GameObject floor;
    public GameObject corridor;
    public GameObject door;

    public GameObject player;

    public List<GameObject> enemies;

    List<Tile> layout;

    public int sizeX;
    public int sizeY;

    int width;
    int height;

	// Use this for initialization
	void Start () {

        layout = new List<Tile>();

        width = 2 * sizeX + 1;
        height = 2 * sizeY + 1;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                layout.Add(new Tile(TileType.wall));
            }
        }

        Generate();

	}

    void Generate()
    {
        PlaceRoom(3, 3, 2, 2,1);
        PlaceRooms(1000000);

        int m_grp = -1;
        List<int> xes = new List<int>();
        List<int> yes = new List<int>();
        for (int i = 0; i < sizeX; i++) xes.Add(i);
        for (int i = 0; i < sizeY; i++) yes.Add(i);
        xes.Shuffle();
        foreach (int i in xes)
        {
            yes.Shuffle();
            foreach (int j in yes)
            {
                Maze(i*2+1, j*2+1,m_grp--);
            }
        }

        MakeConnections();

        //*
        bool removed = true;
        while (removed)
        {
            removed = false;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    removed = removed || RemoveDead(i, j);
                }
            }
        }
        /**/



        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile t = getTile(i, j);
                GameObject go = null;
                GameObject en = null;
                switch (t.type)
                {
                    case TileType.wall:
                        go = Instantiate<GameObject>(wall);
                        break;
                    case TileType.corridor:
                        go = Instantiate<GameObject>(corridor);
                        if (Random.value < 0.1)
                            en = Instantiate<GameObject>(enemies.PickRandom());
                        break;
                    case TileType.room  :
                        go = Instantiate<GameObject>(floor);
                        if (Random.value < 0.1)
                            en = Instantiate<GameObject>(enemies.PickRandom());
                        break;
                    case TileType.door:
                        go = Instantiate<GameObject>(door);
                        break;
                }
                if (go != null)
                {
                    go.transform.parent = this.transform;
                    go.transform.position = new Vector3(i, j, 0);
                }
                if (en != null)
                {
                    en.transform.position = new Vector3(i, j, 0);
                    if (Vector3.Distance(en.transform.position, player.transform.position) < 4)
                    {
                        Destroy(en.gameObject);
                    }
                }
            }
        }
    }

    void PlaceRooms(int max_failed)
    {
        int failed = 0;
        int group = 3;
        while (failed < max_failed)
        {
            int w = Random.Range(2, 5);
            int h = Random.Range(2, 5);
            int x = Random.Range(0, sizeX - w);
            int y = Random.Range(0, sizeY - h);
            x = x * 2 + 1;
            y = y * 2 + 1;
            w = w * 2;
            h = h * 2;
            bool nope = false;
            for (int i = x; i <= x + w; i++)
            {
                for (int j = y; j <= y + h; j++)
                {
                    if (getTile(i, j).type != TileType.wall)
                    {
                        nope = true;
                        break;
                    }
                }
                if (nope) break;
            }
            if (nope)
                failed++;
            else
            {
                PlaceRoom(x, y, w, h, group++);
            }
        }
    }

    void PlaceRoom(int x, int y, int w, int h, int grp = 0)
    {
        for (int i = x; i <= x + w; i++)
        {
            for (int j = y; j <= y + h; j++)
            {
                setTile(i, j, new Tile(TileType.room, grp));
            }
        }
    }

    void Maze(int x, int y, int grp = 0)
    {
        if (getTile(x, y).type != TileType.wall)
            return;

        setTile(x, y, new Tile(TileType.corridor, grp));

        List<Vector2> n_list = new List<Vector2>();
        n_list.Add(new Vector2(1, 0));
        n_list.Add(new Vector2(-1, 0));
        n_list.Add(new Vector2(0, 1));
        n_list.Add(new Vector2(0, -1));
        n_list.Shuffle();

        foreach (Vector2 v in n_list)
        {
            int nx = x + Mathf.RoundToInt(v.x) * 2;
            int ny = y + Mathf.RoundToInt(v.y) * 2;
            int vx = x + Mathf.RoundToInt(v.x);
            int vy = y + Mathf.RoundToInt(v.y);
            if (
                nx>=0 && nx<width &&
                ny>=0 && ny<height &&
                getTile(nx, ny).type==TileType.wall
                ){
                setTile(vx, vy, new Tile(TileType.corridor, grp));
                Maze(nx, ny, grp);
            }
        }

    }

    void MakeConnections()
    {
        List<Bridge> candidates = FindCandidates();
        Connector<int> connect = new Connector<int>();

        candidates.Shuffle();

        foreach (Bridge cand in candidates)
        {
            int c1 = cand.groupA;
            int c2 = cand.groupB;
            if (!connect.isConnected(c1, c2))
            {
                setTile(cand.x, cand.y, new Tile(TileType.door, 0));
                connect.Connect(c1, c2);
            }
        }

    }

    List<Bridge> FindCandidates()
    {
        List<Bridge> candidates = new List<Bridge>();

        List<Vector2> n_list = new List<Vector2>();
        n_list.Add(new Vector2(1, 0));
        n_list.Add(new Vector2(-1, 0));
        n_list.Add(new Vector2(0, 1));
        n_list.Add(new Vector2(0, -1));

        for (int i = 2; i < width-2; i++)
        {
            for (int j = 2; j < height - 2; j++)
            {
                if (getTile(i,j).type == TileType.room)
                {
                    foreach (Vector2 v in n_list)
                    {
                        int nx = i + Mathf.RoundToInt(v.x) * 2;
                        int ny = j + Mathf.RoundToInt(v.y) * 2;
                        int vx = i + Mathf.RoundToInt(v.x);
                        int vy = j + Mathf.RoundToInt(v.y);
                        if (
                            getTile(vx, vy).type == TileType.wall &&
                            getTile(nx, ny).type != TileType.wall
                            )
                        {
                            Bridge b;
                            b.x = vx;
                            b.y = vy;
                            b.groupA = getTile(i, j).group;
                            b.groupB = getTile(nx, ny).group;
                            candidates.Add(b);
                        }
                    }
                }
            }
        }

        return candidates;
    }

    bool RemoveDead(int x, int y)
    {
        if (getTile(x, y).type == TileType.corridor || getTile(x, y).type == TileType.door)
        {
            List<Vector2> n_list = new List<Vector2>();
            n_list.Add(new Vector2(1, 0));
            n_list.Add(new Vector2(-1, 0));
            n_list.Add(new Vector2(0, 1));
            n_list.Add(new Vector2(0, -1));
            int walls = 0;
            foreach (Vector2 v in n_list)
            {
                int vx = x + Mathf.RoundToInt(v.x);
                int vy = y + Mathf.RoundToInt(v.y);
                if (getTile(vx, vy).type == TileType.wall)
                {
                    walls++;
                }
                if (walls >= 3)
                {
                    setTile(x, y, new Tile(TileType.wall));
                    foreach (Vector2 v2 in n_list)
                    {
                        int v2x = x + Mathf.RoundToInt(v2.x);
                        int v2y = y + Mathf.RoundToInt(v2.y);
                        if (getTile(v2x, v2y).type == TileType.corridor || getTile(v2x, v2y).type == TileType.door)
                            RemoveDead(v2x, v2y);
                    }
                    return true;
                }
            }


        }
        return false;
    }

    Tile getTile(int x, int y)
    {
        return layout[(x) + (y) * width];
    }

    void setTile(int x, int y, Tile t)
    {
        layout[(x) + (y) * width] = t;
    }

}

public class Connector<T>
{
    Dictionary<T, List<T>> data;
    public Connector()
    {
        data = new Dictionary<T, List<T>>();
    }

    public bool isConnected(T a, T b)
    {
        if (data.ContainsKey(a))
        {
            foreach (T t in data[a])
            {
                if (t.Equals(b))
                    return true;
            }
        }
        if (data.ContainsKey(b))
        {
            foreach (T t in data[b])
            {
                if (t.Equals(a))
                    return true;
            }
        }
        return false;
    }

    public void Connect(T a, T b)
    {
        if (!data.ContainsKey(a))
        {
            data[a] = new List<T>();
        }
        if (!data.ContainsKey(b))
        {
            data[b] = new List<T>();
        }
        if (!isConnected(a, b))
        {
            data[a].Add(b);
            data[b].Add(a);
            T[] cln = new T[data[a].Count];
            data[a].CopyTo(cln);
            foreach (T t in cln)
                Connect(t, b);
            T[] cln2 = new T[data[b].Count];
            data[b].CopyTo(cln2);
            foreach (T t in cln2)
                Connect(t, a);
        }
    }

}
