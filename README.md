# Dotnet-RabbitMqChannelPoll

inject ObjectPool<IModel>, and use it

var channel = pool.Get();
try
{
    channel.BasicPublish(...);
}
finally
{
    pool.Return(channel);
}