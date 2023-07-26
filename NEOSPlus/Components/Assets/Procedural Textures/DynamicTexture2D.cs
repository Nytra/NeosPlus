using System;
using System.Linq;
using BaseX;
using CodeX;
using FrooxEngine.LogiX;
using FrooxEngine.UIX;

namespace FrooxEngine
{
    [Category("Assets/Procedural Textures")]
    public class DynamicTexture2D : ProceduralTexture
    {
        [HideInInspector]
        public readonly SyncList<Sync<color>> PixelArray;

        //public readonly SyncArray<color> PixelArray;

        //public readonly Sync<Bitmap2D> Bitmap;

        //protected override int2 GenerateSize => Bitmap.Value.Size;

        private int GetPixelIndex(int x, int y)
        {
            return y * tex2D.Size.x + x;
        }

        public bool SetPixel(int x, int y, color c)
        {
            if (x < 0 || x >= tex2D.Size.x || y < 0 || y >= tex2D.Size.y)
            {
                return false;
            }
            if (GetPixel(x, y) != c)
            {
                PixelArray[GetPixelIndex(x, y)].Value = c;
                base.OnChanges();
            }
            return true;
        }

        public color GetPixel(int x, int y)
        {
            if (x < 0 || x >= tex2D.Size.x || y < 0 || y >= tex2D.Size.y)
            {
                return color.Clear;
            }
            return PixelArray[GetPixelIndex(x, y)];
        }

        public void ShiftHue(float shift)
        {
            this.RunSynchronously(() => 
            {
                foreach(Sync<color> sync in PixelArray)
                {
                    color c = sync.Value;
                    ColorHSV hsv = new ColorHSV(in c);
                    hsv.h += shift;
                    sync.Value = hsv.ToRGB();
                }
            });
            base.OnChanges();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            Sync<int2> size = Size;
            int2 v = int2.One;
            size.Value = v * 4;
            FilterMode.Value = TextureFilterMode.Point;
        }

        protected override void OnAttach()
        {
            base.OnAttach();
        }

        protected override void PrepareAssetUpdateData()
        {
        }

        protected override void ClearTextureData()
        {
        }

        private void Resize(int width, int height)
        {
            this.RunSynchronously(() =>
            {
                while (PixelArray.Count < width * height)
                {
                    PixelArray.Add().Value = color.White;
                }
                while (PixelArray.Count > width * height)
                {
                    PixelArray.RemoveAt(PixelArray.Count - 1);
                }
            });
        }

        protected override void UpdateTextureData(Bitmap2D tex2D)
        {
            if (PixelArray.Count != tex2D.Size.x * tex2D.Size.y)
            {
                this.RunSynchronously(() =>
                {
                    Resize(tex2D.Size.x, tex2D.Size.y);
                });
                this.RunSynchronously(() => UpdateTextureData(tex2D));
            }
            else
            {
                for (int i = 0; i < tex2D.Size.y; i++)
                {
                    for (int j = 0; j < tex2D.Size.x; j++)
                    {
                        tex2D.SetPixel(j, i, GetPixel(j, i), 0);
                    }
                }
            }
        }
    }
}