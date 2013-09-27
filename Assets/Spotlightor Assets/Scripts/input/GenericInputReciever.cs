using UnityEngine;
using System.Collections;

public abstract class GenericInputReciever:EnhancedMonoBehaviour
{
    /// <summary>
    /// 外部输入了泛用方向型Input，在这个方法中不同的Reciever应该做出相应的反馈
    /// </summary>
    /// <param name="x">水平方向input</param>
    /// <param name="y">垂直方向input</param>
    /// <param name="z">纵深方向input</param>
    public abstract void OnDirectionInput(float x, float y, float z);

    /// <summary>
    /// 外部输入了泛用序列号型Input，在中合格方法中不同的Reciever应对不同的序号做出相应的反馈
    /// </summary>
    /// <param name="index">序号，e.g. 0/1/2/3...</param>
    public abstract void OnIndexInput(uint index);
}