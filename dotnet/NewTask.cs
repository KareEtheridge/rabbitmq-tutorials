using RabbitMQ.Client;

class Program {
    static void Main(string[] args) {
        ConnectionFactory factory = new ConnectionFactory();
        factory.HostName = "localhost";
        using (IConnection connection = factory.CreateConnection())
        using (IModel channel = connection.CreateModel()) {
            channel.QueueDeclare("task_queue", true, false, false, null);

            string message = (args.Length > 0) ? System.String.Join(" ", args)
                                               : "Hello World!";
            byte[] body = System.Text.Encoding.UTF8.GetBytes(message);

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish("", "task_queue", properties, body);
            System.Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}
