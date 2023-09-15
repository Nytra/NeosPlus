﻿using BaseX;
using FrooxEngine.LogiX;

// Rad was here
namespace FrooxEngine.LogiX.Math
{
    [Category("LogiX/NeosPlus/Mesh/Vertex")]
    public class GetVertex : LogixOperator<Vertex>
    {
        public readonly Input<IAssetProvider<Mesh>> Mesh;
        public readonly Input<int> Index;

        public override Vertex Content
        {
            get
            {
                var mesh = Mesh.Evaluate();
                return mesh?.Asset == null ? new Vertex() : mesh.Asset.Data.GetVertex(Index.Evaluate());
            }
        }
    }
}