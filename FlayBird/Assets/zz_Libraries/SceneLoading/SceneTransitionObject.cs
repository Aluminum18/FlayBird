using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SceneTransitionObject : MonoBehaviour
{
    public static event Action OnStartCloseAnim;
    public static event Action OnFinishCloseAnim;
    public static event Action OnStartOpenAnim;
    public static event Action OnFinishOpenAnim;

    public void DoCloseSceneAnim(float duration)
    {
        OnStartCloseAnim?.Invoke();
        AnimateClose(duration);
    }

    public void DoOpenSceneAnim(float duration)
    {
        OnStartOpenAnim?.Invoke();
        AnimateOpen(duration);
    }

    protected virtual void AnimateClose(float duration)
    {
        Debug.Log("AnimateClose");
        Task.Delay((int)(duration * 1000f)).ContinueWith(delayTask =>
        {
            OnFinishCloseAnim?.Invoke();
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    protected virtual void AnimateOpen(float duration)
    {
        Task.Delay((int)(duration * 1000f)).ContinueWith(delayTask =>
        {
            OnFinishOpenAnim?.Invoke();
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
}
