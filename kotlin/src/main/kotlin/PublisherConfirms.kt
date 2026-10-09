import com.rabbitmq.client.ConnectionFactory
import kotlin.time.ExperimentalTime
import kotlin.time.measureTime

const val MESSAGE_COUNT = 50_000

fun createConnection() = ConnectionFactory().apply {
    host = "localhost"
    username = "guest"
    password = "guest"
}.newConnection()

@OptIn(ExperimentalTime::class)
fun publishMessagesIndividually() {
    createConnection().use { connection ->
        val channel = connection.createChannel()
        val queue = java.util.UUID.randomUUID().toString()
        channel.queueDeclare(queue, false, false, true, null)
        channel.confirmSelect()

        val duration = measureTime {
            for (i in 0 until MESSAGE_COUNT) {
                val body = i.toString()
                channel.basicPublish("", queue, null, body.toByteArray())
                channel.waitForConfirmsOrDie(5_000)
            }
        }
        println("Published %,d messages individually in %s".format(MESSAGE_COUNT, duration))
    }
}

@OptIn(ExperimentalTime::class)
fun publishMessagesInBatch() {
    createConnection().use { connection ->
        val channel = connection.createChannel()
        val queue = java.util.UUID.randomUUID().toString()
        channel.queueDeclare(queue, false, false, true, null)
        channel.confirmSelect()

        val batchSize = 100
        var outstandingCount = 0

        val duration = measureTime {
            for (i in 0 until MESSAGE_COUNT) {
                val body = i.toString()
                channel.basicPublish("", queue, null, body.toByteArray())
                outstandingCount++

                if (outstandingCount == batchSize) {
                    channel.waitForConfirmsOrDie(5_000)
                    outstandingCount = 0
                }
            }
            if (outstandingCount > 0) {
                channel.waitForConfirmsOrDie(5_000)
            }
        }
        println("Published %,d messages in batch in %s".format(MESSAGE_COUNT, duration))
    }
}

fun main() {
    publishMessagesIndividually()
    publishMessagesInBatch()
}
