using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Starting Sakura server...");

var lastInSeconds = HighResolutionTimeInSeconds();
var tickLengthInSeconds = 1d / 60.0d;
var accumulator = 0d;

var listener = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);
var ipEndPoint = new IPEndPoint(
    0,
    32900);
listener.Bind(ipEndPoint);
listener.Listen(100);

while (true)
{
    var nowInSeconds = HighResolutionTimeInSeconds();
    var elapsedSeconds = nowInSeconds - lastInSeconds;
    lastInSeconds = nowInSeconds;
    accumulator += elapsedSeconds;

    ReceiveNetworkMessages();

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

void ReceiveNetworkMessages()
{
    var handler = listener.Accept();
    if (handler == null)
        return;
    var buffer = new byte[1_024];
    var bytesReceived = handler.Receive(buffer, SocketFlags.None);
    var response = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
    Console.WriteLine(response);

    var message = "Hello, client!";
    var messageBytes = Encoding.UTF8.GetBytes(message);
    handler.Send(messageBytes, SocketFlags.None);
}
