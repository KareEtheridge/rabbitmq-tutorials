#!/usr/bin/env ruby
require 'bunny'

connection = Bunny.new(automatically_recover: false)
connection.start

channel = connection.create_channel

# Enable publisher confirms on the channel
channel.confirm_select

# Declare a server-named, exclusive queue
queue = channel.queue('', exclusive: true)

MESSAGE_COUNT = 100

puts "Publishing #{MESSAGE_COUNT} messages with publisher confirms..."

MESSAGE_COUNT.times do |i|
  body = i.to_s
  channel.default_exchange.publish(
    body,
    routing_key: queue.name,
    message_id: i.to_s
  )
  puts " [x] Sent and confirmed message #{i}"
end

# Wait for all confirms
channel.wait_for_confirms
channel.close
connection.close

puts "\n [*] Published #{MESSAGE_COUNT} messages successfully!"
