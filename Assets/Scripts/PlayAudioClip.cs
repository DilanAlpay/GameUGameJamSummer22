using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    public void PlayClip()
    {
        SoundManager.Instance.Play(clip);
    }
}
