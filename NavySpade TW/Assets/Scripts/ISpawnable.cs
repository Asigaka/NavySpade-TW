public interface ISpawnable
{
    float MinSpawnPeriodicity { get; }
    float MaxSpawnPeriodicity { get; }
    int StartCount { get; }
    int MaxSpawnedCountOnMap { get; }
}