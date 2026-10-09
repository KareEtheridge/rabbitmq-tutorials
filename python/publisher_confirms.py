#!/usr/bin/env python
import pika
import uuid


def main():
    connection = pika.BlockingConnection(
        pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()

    # Enable publisher confirms
    channel.confirm_delivery()

    # Declare a server-named, exclusive queue
    result = channel.queue_declare(queue='', exclusive=True)
    queue_name = result.method.queue

    MESSAGE_COUNT = 100

    try:
        for i in range(MESSAGE_COUNT):
            body = str(i).encode()
            channel.basic_publish(
                exchange='',
                routing_key=queue_name,
                body=body,
                properties=pika.BasicProperties(
                    message_id=str(uuid.uuid4()),
                ),
                mandatory=True,
            )
            print(f" [x] Sent and confirmed message {i}")

    except pika.exceptions.UnroutableError:
        print('Message was returned')
    except pika.exceptions.NackError:
        print('Message was nack-ed')

    connection.close()
    print(f" [*] Published {MESSAGE_COUNT} messages successfully!")


if __name__ == '__main__':
    main()
