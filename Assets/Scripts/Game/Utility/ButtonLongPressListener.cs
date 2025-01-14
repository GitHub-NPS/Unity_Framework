using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using com.unimob.mec;
using System;
using UnityEngine.Events;

public class ButtonLongPressListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Vector2 delay = new Vector2(0.05f, 0.15f);
    [SerializeField] private float decrease = 0.005f;

    private CoroutineHandle handle;
    private Func<bool> condition;
    protected float second = 0.1f;
    private Action action;

    private void Start()
    {

    }

    public void SetTime(Vector2 delay, float decrease)
    {
        this.delay = delay;
        this.decrease = decrease;
    }

    public void Set(Action action, Func<bool> condition)
    {
        this.action = action;
        this.condition = condition;
    }

    private void OnDisable()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        second = delay.y;

        if (handle.IsValid) Timing.KillCoroutines(handle);
        handle = Timing.RunCoroutine(_Update());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    private IEnumerator<float> _Update()
    {
        while (condition != null && action != null)
        {
            if (!condition.Invoke())
            {
                second = delay.y;

                Timing.KillCoroutines(handle);
                handle = default;
                break;
            }
            iAction();
            yield return Timing.WaitForSeconds(second = Mathf.Max(delay.x, second - decrease));
        }
    }

    protected virtual void iAction()
    {
        action?.Invoke();
    }
}
