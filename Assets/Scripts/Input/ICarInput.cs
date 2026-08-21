namespace SimpleCarPhysics.Input
{
    public interface ICarInput
    {
        /// <summary>Negative = reverse, positive = forward, range roughly [-1, 1].</summary>
        float Throttle { get; }
    }
}
