using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using System;
using System.Threading.Tasks;
using CodeX;

[Category(new string[] { "LogiX/Assets" })]
public class SetDynamicTexture2DPixel : LogixNode
{
	public readonly Input<DynamicTexture2D> Texture;

	public readonly Input<int2> Position;

	public readonly Input<color> Color;

	public readonly Input<int> MipLevel;

	public readonly Impulse OnDone;

	public readonly Impulse OnFailed;

	[ImpulseTarget]
	public void SetPixel()
	{
        base.OnEvaluate();
        DynamicTexture2D texture = Texture.EvaluateRaw();
		color c = Color.EvaluateRaw();
		int2 position = Position.EvaluateRaw();
		int mipLevel = MipLevel.EvaluateRaw();
		if (texture != null)
		{
            texture.SetPixel(position.x, position.y, c, mipLevel);
            OnDone.Trigger();
        }
		else
		{
			OnFailed.Trigger();
		}
	}
}