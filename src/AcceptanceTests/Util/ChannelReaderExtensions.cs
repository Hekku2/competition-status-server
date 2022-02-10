using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AcceptanceTests.Util;

public static class ChannelReaderExtensions
{
    public static async Task ReadUntilStopped<T>(this ChannelReader<T> channel, Action<T> action, CancellationToken stoppingToken)
    {
        while (await channel.WaitToReadAsync(stoppingToken))
        {
            while (channel.TryRead(out var receivedEvent))
            {
                action(receivedEvent);
            }
        }
    }
}
