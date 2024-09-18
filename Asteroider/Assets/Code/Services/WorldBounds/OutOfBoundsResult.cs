namespace Code.Services.WorldBounds
{
    public struct OutOfBoundsResult
    {
        public bool isOutOfHorizontal;
        public bool isOutOfVertical;

        public bool IsInside() => 
            !isOutOfHorizontal && !isOutOfVertical;
            
        public bool IsOutside() => 
            isOutOfHorizontal || isOutOfVertical;

        public bool IsOutOnlyOfHorizontal() => 
            isOutOfHorizontal && !isOutOfVertical;
    }
}