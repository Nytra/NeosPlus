using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using System;
using FrooxEngine.UIX;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using CodeX;

[Category(new string[] { "LogiX/Assets" })]
public class SetStaticTexture2DPixel : LogixNode
{
    public readonly Input<StaticTexture2D> Texture;

    public readonly Input<int2> Position;

    public readonly Input<color> Color;

    public readonly Input<int> MipLevel;

    public readonly Impulse OnStarted;

    public readonly Impulse OnFinished;

    public readonly Impulse OnFailed;

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
                StartTask(async delegate
                {
                    OnStarted.Trigger();

                    bool flag = false;

                    //UniLog.Log("Hello!");
                    try
                    {
                        Task<Bitmap2D> thing = texture.Asset.GetOriginalTextureData();
                        await thing;
                        color existingColor = thing.Result.GetPixel(position.x, position.y, mipLevel);
                        //UniLog.Log("existingColor: " + existingColor.ToString());
                        if (existingColor != c)
                        {
                            //Action<IChangeable> func = null;
                            //func = delegate
                            //{
                            //    OnDone.Trigger();
                            //    texture.URL.Changed -= func;
                            //};
                            //texture.URL.Changed += func;
                        }
                        else
                        {
                            flag = true;
                        }

                        if (flag)
                        {
                            OnFinished.Trigger();
                        }
                        else
                        {
                            //UniLog.Log("Hello2!");
                            texture.Process((B) => { B.SetPixel(position.x, position.y, c, mipLevel); return B; }, null);

                            while (texture.Asset != null)
                            {
                                // wait until asset becomes null
                                //UniLog.Log("In wait loop1");
                                await Task.Delay(1);
                            }
                            while (texture.Asset == null)
                            {
                                // wait until asset becomes not null
                                //niLog.Log("In wait loop2");
                                await Task.Delay(1);
                            }
                            OnFinished.Trigger();
                        }
                        //UniLog.Log("Hello3!");
                    }
                    catch (Exception e)
                    {
                        //UniLog.Error("Error inside try catch 2");
                        UniLog.Error(e.ToString());
                        OnFailed.Trigger();
                    }
                });
            }
            else
            {
                OnFailed.Trigger();
            }
        }
        catch (Exception e)
        {
            //UniLog.Error("Error inside try catch 1");
            UniLog.Error(e.ToString());
            OnFailed.Trigger();
        }
    }
}