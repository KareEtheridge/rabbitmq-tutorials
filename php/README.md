# PHP code for RabbitMQ tutorial #

Here you can find a PHP code examples from [RabbitMQ
tutorials](http://www.rabbitmq.com/getstarted.html).


## Requirements ##

To run the examples you need a running RabbitMQ server.

Additionaly you need `PHP 5.3` and `php-amqplib`. To get this
dependencies on Ubuntu type:

    sudo apt-get install git-core php5-cli
    git clone http://github.com/tnc/php-amqplib.git lib/php-amqplib


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

