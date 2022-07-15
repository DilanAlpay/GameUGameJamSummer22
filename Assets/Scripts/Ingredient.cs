using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [Range(1,4)]
    public int order;
    public GameEvent fadeOut;
    public GameEvent startDialog;
    public CounterObj counter;
    public Dialog dialog;
    private bool _showOff;
    private GrowBall _ball;

    private void OnTriggerEnter(Collider other)
    {
        GetAbsorbed(other.gameObject);
    }

    public void GetAbsorbed(GameObject sphere)
    {
        _ball = sphere.GetComponentInParent<GrowBall>();

        if (!_ball) return;

        fadeOut.Call();
        _showOff = true;
    }

    public void Absorb()
    {
        if (!_showOff) return;
        counter.Add(1);
        Destroy(GetComponent<Collider>());
        _ball.GetItem(transform, order);
        _ball.IncreaseSize();
    }

    public void ShowOff()
    {
        if (!_showOff) return;
        startDialog.StartDialog(dialog);
    }
}
