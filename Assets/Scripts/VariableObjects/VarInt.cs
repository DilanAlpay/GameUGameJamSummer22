using UnityEngine;
[CreateAssetMenu(menuName ="Variables/Int")]
public class VarInt: VarBase<int>
{
    public override void Change(int obj)
    {
        value += obj;
    }
}
