using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using System;
using FrooxEngine.UIX;

[Category(new string[] { "LogiX/Assets" })]
public class SetStaticTexture2DPixel : LogixNode
{
    public readonly Input<StaticTexture2D> Texture;

    public readonly Input<int2> Position;

    public readonly Input<color> Color;

    public readonly Input<int> MipLevel;

    public readonly Impulse OnStarted;

    public readonly Impulse OnDone;

    public readonly Impulse OnFail;

    [ImpulseTarget]
    public void SetPixel()
    {
        StaticTexture2D texture = Texture.EvaluateRaw();
        color c = Color.EvaluateRaw();
        int2 position = Position.EvaluateRaw();
        int mipLevel = MipLevel.EvaluateRaw();
        try
        {
            if (texture != null)
            {
                //Action<IChangeable> func = null;
                //func = delegate
                //{
                //    OnDone.Trigger();
                //    texture.URL.Changed -= func;
                //};
                //texture.URL.Changed += func;

                OnStarted.Trigger();
                texture.Process((B) => { B.SetPixel(position.x, position.y, c, mipLevel); return B; }, null);
                OnDone.Trigger();

                // TODO:
                // Make OnDone trigger when the asset URL changes or if the pixel color is already matching the input color then it can pulse immediately
                // Basically it should only pulse when the asset actually updated (if it needed to update)
            }
            else
            {
                OnFail.Trigger();
            }
        }
        catch (Exception e)
        {
            UniLog.Error(e.ToString());
            OnFail.Trigger();
        }
    }
}