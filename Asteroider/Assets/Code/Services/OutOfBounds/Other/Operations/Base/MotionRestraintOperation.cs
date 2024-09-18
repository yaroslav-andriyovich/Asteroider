namespace Code.Services.OutOfBounds.Other.Operations.Base
{
    public enum MotionRestraintOperation
    {
        None = 0,
        LimitInsideBounds = 1,
        LimitYAndInfiniteX,
        InvertXOrClear,
        Clear,
        InfiniteXOrClear
    }
}