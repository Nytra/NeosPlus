using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using System;
using System.Threading.Tasks;
using CodeX;

[Category(new string[] { "LogiX/Assets/Dynamic Texture 2D" })]
public class SetDynamicTexture2DPixel : LogixNode
{
	public readonly Input<DynamicTexture2D> Texture;

	public readonly Input<int2> Position;

	public readonly Input<color> Color;

	public readonly Impulse OnDone;

	public readonly Impulse OnFailed;

	[ImpulseTarget]
	public void SetPixel()
	{
        base.OnEvaluate();
        DynamicTexture2D texture = Texture.Evaluate();
		color c = Color.Evaluate();
		int2 position = Position.Evaluate();
		if (texture != null)
		{
            bool result = texture.SetPixel(position.x, position.y, c);
            if (result)
            {
                OnDone.Trigger();
            }
            else
            {
                OnFailed.Trigger();
            }
        }
		else
		{
			OnFailed.Trigger();
		}
	}
}