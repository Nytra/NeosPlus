using System;
using BaseX;
using CodeX;
using FrooxEngine.LogiX;
using FrooxEngine.UIX;

namespace FrooxEngine
{
    [Category("Assets/Procedural Textures")]
    public class DynamicTexture2D : ProceduralTexture
    {
        public void SetPixel(int x, int y, color c, int mipLevel)
        {
            tex2D.SetPixel(x, y, c, mipLevel);
            base.OnChanges();
        }

        public color GetPixel(int x, int y, int mipLevel=0)
        {
            return tex2D.GetPixel(x, y, mipLevel);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            Sync<int2> size = Size;
            int2 v = int2.One;
            size.Value = v * 4;
        }

        protected override void PrepareAssetUpdateData()
        {
        }

        protected override void ClearTextureData()
        {
        }

        protected override void UpdateTextureData(Bitmap2D tex2D)
        {
        }
    }
}