namespace Code.Entities.Components
{
    public enum ScreenLimitOperation
    {
        None = 0,
        LimitMotion = 1,
        LimitYAndInfiniteXMotion,
        InvertXMotionOrClear,
        Clear
    }
}