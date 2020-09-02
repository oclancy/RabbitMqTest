namespace RabbitClient
{
    public interface IEndpoint
    {
        void Publish(string message);
        //void Subscribe<T>( string topic, TMessage)
    }
}
