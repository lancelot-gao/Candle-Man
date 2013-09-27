using UnityEngine;
using System.Collections;

/// <summary>
/// 好的交互至少需要流畅平滑的Transition
/// </summary>
public interface ITransition
{
    void TransitionIn();
    void TransitionIn(bool instant);

    void TransitionOut();
    void TransitionOut(bool instant);
}
