using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SakuraServer
{
    internal class Server
    {
        private bool running;

        internal void Run()
        {
            Console.WriteLine("Starting Sakura server...");
            var lastInSeconds = HighResolutionTimeInSeconds();
            var tickLengthInSeconds = 1d / 60.0d;
            var accumulator = 0d;
            running = true;

            var listener = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            Task.Run(async () =>
            {
                await AcceptIncomingConnections(
                    listener,
                    cancellationToken);
            });
            Task.Run(async () =>
            {
                await ReadConsoleInputAsync(cancellationToken);
            });

            while (running)
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

            cancellationTokenSource.Cancel();
            listener.Close();
            Console.WriteLine("Stopping Sakura server...");
        }

        private Task ReadConsoleInputAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var nextLine = Console.ReadLine();
                if (nextLine == "stop")
                    running = false;
                else
                    Console.WriteLine($"Unrecognized command '{nextLine}'");
            }
            return Task.CompletedTask;
        }

        private async Task AcceptIncomingConnections(
            Socket listener,
            CancellationToken cancellationToken)
        {
            var ipEndPoint = new IPEndPoint(
                0,
                32900);
            listener.Bind(ipEndPoint);
            listener.Listen(100);
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("[Listener] Listening...");
                await Accept(
                    listener,
                    cancellationToken);
            }
        }

        private async Task Accept(
            Socket listener,
            CancellationToken cancellationToken)
        {
            try
            {
                // InvalidOperationException will not be thrown since we
                // call Bind(Endpoint) and Listen(Int32) before this.
                // ObjectDisposedException will not be thrown since this socket
                // is never closed when this method is called.
                var connection = await listener.AcceptAsync(cancellationToken);
                await HandleConnection(
                    connection,
                    cancellationToken);
            }
            catch (SocketException socketException)
            {
                Console.WriteLine($"An unexpected {nameof(SocketException)}"
                    + " has occured");
                Console.WriteLine(socketException.ToString());
                running = false;
            }
        }

        private async Task HandleConnection(
            Socket connection,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("[Listener] Accepted connection");
            var buffer = new byte[1_024];
            try
            {
                // ObjectDisposedException will not be thrown since this socket
                // will never be closed.
                var bytesReceived = await connection.ReceiveAsync(
                    buffer,
                    SocketFlags.None,
                    cancellationToken);
                var response = Encoding.UTF8.GetString(
                    buffer,
                    0,
                    bytesReceived);
                Console.WriteLine(
                    $"[Listener] Client sent: '{response}'");

                var serverMessage = "Hello, client!<|EOM|>";
                var serverMessageBytes = Encoding.UTF8.GetBytes(serverMessage);
                await connection.SendAsync(
                    serverMessageBytes,
                    SocketFlags.None,
                    cancellationToken);
            }
            catch (SocketException socketException)
            {
                Console.WriteLine($"An unexpected {nameof(SocketException)}"
                    + " has occured.");
                Console.WriteLine(socketException.ToString());
                running = false;
            }
        }

        private void TickSimulation()
        {
        }

        private static double HighResolutionTimeInSeconds()
        {
            long nowInDotNetTicks = DateTime.UtcNow.Ticks;
            // One .NET tick is 100 nanoseconds
            long nowInNanoseconds = nowInDotNetTicks * 100;
            double elapsedSeconds = (double)(nowInNanoseconds / 1000000000d);
            return elapsedSeconds;
        }
    }
}
