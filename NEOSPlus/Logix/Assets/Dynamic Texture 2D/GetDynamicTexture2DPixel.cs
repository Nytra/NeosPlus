using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using System;
using System.Threading.Tasks;
using CodeX;

[Category(new string[] { "LogiX/Assets/Dynamic Texture 2D" })]
public class GetDynamicTexture2DPixel : LogixNode
{
	public readonly Input<DynamicTexture2D> Texture;

	public readonly Input<int2> Position;

    public readonly Output<color> Color;

    protected override void OnEvaluate()
    {
        base.OnEvaluate();
        DynamicTexture2D texture = Texture.Evaluate();
        int2 position = Position.Evaluate();
        if (texture != null)
        {
            Color.Value = texture.GetPixel(position.x, position.y);
        }
        else
        {
            Color.Value = color.Clear;
        }
    }
}