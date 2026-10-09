<?php

require_once __DIR__ . '/vendor/autoload.php';
use PhpAmqpLib\Connection\AMQPStreamConnection;
use PhpAmqpLib\Message\AMQPMessage;

$connection = new AMQPStreamConnection('localhost', 5672, 'guest', 'guest');
$channel = $connection->channel();

// Enable publisher confirms on the channel
$channel->confirm_select();

// Declare a server-named, exclusive queue
[$queue_name, ,] = $channel->queue_declare('', false, false, true, false);

$message_count = 100;

echo "Publishing {$message_count} messages with publisher confirms...\n";

for ($i = 0; $i < $message_count; $i++) {
    $msg = new AMQPMessage((string)$i, ['message_id' => (string)$i]);
    $channel->basic_publish($msg, '', $queue_name);
    echo " [x] Sent and confirmed message {$i}\n";
}

// Wait for all confirms
$channel->wait_for_confirms();

$channel->close();
$connection->close();

echo "\n [*] Published {$message_count} messages successfully!\n";
?>
