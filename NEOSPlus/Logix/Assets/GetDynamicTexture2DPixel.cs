using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using System;
using System.Threading.Tasks;
using CodeX;

[Category(new string[] { "LogiX/Assets" })]
public class GetDynamicTexture2DPixel : LogixNode
{
	public readonly Input<DynamicTexture2D> Texture;

	public readonly Input<int2> Position;

    public readonly Input<int> MipLevel;

    public readonly Output<color> Color;

    protected override void OnEvaluate()
    {
        base.OnEvaluate();
        DynamicTexture2D texture = Texture.EvaluateRaw();
        int2 position = Position.EvaluateRaw();
        int mipLevel = MipLevel.EvaluateRaw();
        if (texture != null)
        {
            Color.Value = texture.GetPixel(position.x, position.y, mipLevel);
        }
        else
        {
            Color.Value = color.Clear;
        }
    }
}