using FrooxEngine;
using FrooxEngine.LogiX;

[NodeName("Invoke Delegate")]
[Category(new string[] { "LogiX/Actions" })]
public class InvokeDelegate : LogixNode
{
    public readonly Input<ISyncDelegate> Target;

    public readonly Impulse OnDone;

    [ImpulseTarget]
    public void Invoke()
    {
        ISyncDelegate syncDelegate = Target.EvaluateRaw();
        if (syncDelegate != null && syncDelegate.Method != null)
        {
            syncDelegate.Method.DynamicInvoke();
            OnDone.Trigger();
        }
    }
}