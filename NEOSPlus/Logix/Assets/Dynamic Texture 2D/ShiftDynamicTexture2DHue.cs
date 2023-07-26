using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using System;
using System.Threading.Tasks;
using CodeX;

[Category(new string[] { "LogiX/Assets/Dynamic Texture 2D" })]
public class ShiftDynamicTexture2DHue : LogixNode
{
	public readonly Input<DynamicTexture2D> Texture;

	public readonly Input<float> Shift;

	public readonly Impulse OnDone;

	public readonly Impulse OnFailed;

	[ImpulseTarget]
	public void ShiftHue()
	{
        base.OnEvaluate();
        DynamicTexture2D texture = Texture.Evaluate();
        float shift = Shift.Evaluate();
		if (texture != null)
		{
            texture.ShiftHue(shift);
            OnDone.Trigger();
        }
		else
		{
			OnFailed.Trigger();
		}
	}
}