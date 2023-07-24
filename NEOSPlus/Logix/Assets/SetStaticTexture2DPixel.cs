using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using System;
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
		if (texture != null)
		{
			StartTask(async delegate
			{
				OnStarted.Trigger();

				try
				{
					Task<Bitmap2D> textureDataTask = texture.Asset.GetOriginalTextureData();
					await textureDataTask;
					color existingColor = textureDataTask.Result.GetPixel(position.x, position.y, mipLevel);

					if (existingColor == c)
					{
						OnFinished.Trigger();
					}
					else
					{
						texture.Process((B) => { B.SetPixel(position.x, position.y, c, mipLevel); return B; }, null);

						while (texture.Asset != null)
						{
							// wait until asset becomes null
							await Task.Delay(1);
						}
						while (texture.Asset == null)
						{
							// wait until asset becomes not null
							await Task.Delay(1);
						}
						OnFinished.Trigger();
					}
				}
				catch (Exception e)
				{
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
}