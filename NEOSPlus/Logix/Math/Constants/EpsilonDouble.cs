﻿using BaseX;

namespace FrooxEngine.LogiX.Math
{
    [Category("LogiX/NeosPlus/Math/Constants")]
    [NodeName("Epsilon Double")]
    public class EpsilonDouble : LogixOperator<double>
    {
        public override double Content => MathX.DOUBLE_EPSILON;
    }
}