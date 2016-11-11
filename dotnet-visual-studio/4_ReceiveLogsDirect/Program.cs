﻿using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    public static void Main( string[] args )
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using( var connection = factory.CreateConnection() )
        using( var channel = connection.CreateModel() )
        {
            channel.ExchangeDeclare( exchange: "direct_logs", type: "direct" );
            var queueName = channel.QueueDeclare().QueueName;

            if( args.Length < 1 )
            {
                Console.Error.WriteLine( "Usage: {0} [info] [warning] [error]", Environment.GetCommandLineArgs()[0] );
                Console.WriteLine( " Press [enter] to exit." );
                Console.ReadLine();
                Environment.ExitCode = 1;
                return;
            }

            foreach( var severity in args )
            {
                channel.QueueBind( queue: queueName, exchange: "direct_logs", routingKey: severity );
            }

            Console.WriteLine( " [*] Waiting for messages. To exit press CTRL+C" );

            var consumer = new QueueingBasicConsumer( channel );
            channel.BasicConsume( queue: queueName, noAck: true, consumer: consumer );

            while( true )
            {
                var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                var body = ea.Body;
                var message = Encoding.UTF8.GetString( body );
                var routingKey = ea.RoutingKey;
                Console.WriteLine( " [x] Received '{0}':'{1}'", routingKey, message );
            }
        }
    }
}
