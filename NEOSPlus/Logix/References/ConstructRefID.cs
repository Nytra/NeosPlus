using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;

[Category(new string[] { "LogiX/References" })]
[NodeName("Construct RefID")]
public class ConstructRefID : LogixOperator<RefID>
{
    public readonly Input<ulong> Position;

    public readonly Input<byte> User;

    public override RefID Content
    {
        get
        {
            ulong position = Position.EvaluateRaw();
            byte user = User.EvaluateRaw();
            return RefID.Construct(position, user);
        }
    }
}