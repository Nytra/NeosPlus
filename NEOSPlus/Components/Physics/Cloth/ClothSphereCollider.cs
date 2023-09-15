﻿namespace FrooxEngine
{
    [Category(new string[] {"NeosPlus/Physics/Cloth"})]
    public class ClothSphereCollider : ClothCollider
    {
        protected override void OnAttach()
        {
            base.OnAttach();
            Radius.Value = 0.5f;
        }
    }
}