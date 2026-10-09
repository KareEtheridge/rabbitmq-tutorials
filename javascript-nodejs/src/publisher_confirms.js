#!/usr/bin/env node

var amqp = require('amqplib');

async function main() {
    const connection = await amqp.connect('amqp://localhost');
    const channel = await connection.createConfirmChannel();

    // Declare a server-named, exclusive queue
    const q = await channel.assertQueue('', { exclusive: true });
    const queueName = q.queue;

    const MESSAGE_COUNT = 100;

    console.log(`Publishing ${MESSAGE_COUNT} messages with publisher confirms...`);

    for (let i = 0; i < MESSAGE_COUNT; i++) {
        const msg = Buffer.from(String(i));
        await new Promise((resolve, reject) => {
            channel.publish('', queueName, msg, {
                messageId: String(i),
            }, (err, ok) => {
                if (err) {
                    reject(new Error(`Message ${i} was nack-ed: ${err.message}`));
                } else {
                    console.log(` [x] Sent and confirmed message ${i}`);
                    resolve();
                }
            });
        });
    }

    await channel.waitForConfirms();
    await channel.close();
    await connection.close();

    console.log(`\n [*] Published ${MESSAGE_COUNT} messages successfully!`);
}

main().catch(err => {
    console.error(err);
    process.exit(1);
});
