using UnityEngine;

[CreateAssetMenu(fileName = "New Color Range", menuName = "Enemies/Create color range")]
public class ColorRange : ScriptableObject
{
    [SerializeField] Color color1 = Color.white;
    [SerializeField] Color color2 = Color.black;

    public void Colorize(SpriteRenderer sr)
    {
        float r = Random.Range(color1.r, color2.r);
        float g = Random.Range(color1.g, color2.g);
        float b = Random.Range(color1.b, color2.b);

        sr.color = new Color(r, g, b, 1);
    }

}