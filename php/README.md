# PHP code for RabbitMQ tutorial #

Here you can find a PHP code examples from [RabbitMQ
tutorials](http://www.rabbitmq.com/getstarted.html).


## Requirements ##

To run the examples you need a running RabbitMQ server.

Additionally you need `PHP 5.3` and `php-amqplib`. To get these
dependencies on Ubuntu type:

    sudo apt-get install git-core php5-cli

Then you can install `php-amqplib` using [Composer](http://getcomposer.org).

To do that install Composer and add it to your path, then run the following command
inside this project folder:

    composer.phar install

## Code

[Tutorial one: "Hello World!"](http://www.rabbitmq.com/tutorial-one-python.html):

    php send.php
    php receive.php


[Tutorial two: Work Queues](http://www.rabbitmq.com/tutorial-two-python.html):

    php new_task.php
    php worker.php


[Tutorial three: Publish/Subscribe](http://www.rabbitmq.com/tutorial-three-python.html)

    php receive_logs.php
    php emit_log.php

[Tutorial four: Routing](http://www.rabbitmq.com/tutorial-four-python.html)

    php receive_logs_direct.php
    php emit_log_direct.php

[Tutorial five: Topics](http://www.rabbitmq.com/tutorial-five-python.html)

    php receive_logs_topic.php
    php emit_log_topic.php

[Tutorial six: RPC](http://www.rabbitmq.com/tutorial-six-python.html):

    php rpc_server.php
    php rpc_client.php
