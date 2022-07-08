using UnityEngine;
[CreateAssetMenu(menuName = "Variables/String")]
public class VarString : VarBase<string>
{
    public override void Change(string obj)
    {
        value += obj;
    }
}
