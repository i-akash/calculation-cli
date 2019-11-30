using System;

namespace Ic.Binding
{
   internal abstract class BoundExpression : BoundNode
    {
        public abstract Type Type { get; }
    }
}