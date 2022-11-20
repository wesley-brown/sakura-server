Console.WriteLine("Starting Sakura server...");

var lastInSeconds = HighResolutionTimeInSeconds();
var tickLengthInSeconds = 1d / 60.0d;
var accumulator = 0d;

while (true)
{
    var nowInSeconds = HighResolutionTimeInSeconds();
    var elapsedSeconds = nowInSeconds - lastInSeconds;
    lastInSeconds = nowInSeconds;
    accumulator += elapsedSeconds;

    while (accumulator >= tickLengthInSeconds)
    {
        TickSimulation();
        accumulator -= tickLengthInSeconds;
    }
}

void TickSimulation()
{
}

double HighResolutionTimeInSeconds()
{
    long nowInDotNetTicks = DateTime.UtcNow.Ticks;
    // One .NET tick is 100 nanoseconds
    long nowInNanoseconds = nowInDotNetTicks * 100;
    double elapsedSeconds = (double)(nowInNanoseconds / 1000000000d);
    return elapsedSeconds;
}
