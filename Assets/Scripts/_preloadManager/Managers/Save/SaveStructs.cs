public enum SaveType
{
    Builder,
    Story
}
[System.Serializable]
public struct Tile
{
    public float x, y, z;
    public int id;
    public Tile(float x, float y, float z, int id)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.id = id;
    }
}
[System.Serializable]
public struct State
{
    //0 = Bomb ; 1 = C4 ; 
    public int[] ammo;
}
