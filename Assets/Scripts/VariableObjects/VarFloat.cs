using UnityEngine;
[CreateAssetMenu(menuName = "Variables/Float")]
public class VarFloat : VarBase<float>
{
    public override void Change(float obj)
    {
        value += obj;
    }
}
