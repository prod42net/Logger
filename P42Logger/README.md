
# **Just another Logger for dotnet.**

Easy to use and highly extendable.

Feel free to use it if you like it.

## P42Logger: A Logger Library for .NET

The P42Logger library is a flexible and extendable logging utility designed with the .NET environment in mind. With this library, you can easily manage logging activities in your applications, control logging levels, and direct logs to different output destinations via log queues.

Key Features
* **Queue-Based Logging**: P42Logger utilizes a queue-based system (List<IP42LogQueue>), allowing you to route logging output to different destinations easily.

* **Flexible Logging Levels**: The library lets you control the granularity of your logging by allowing you to set different logging levels.

* **Extendibility**: P42Logger uses the IP42LogQueue interface, which means you can implement your own log queue classes to customize the logging process.

Class Overview and Usage

P42Logger Class

Here is a basic rundown on how to use the P42Logger class.

// Import necessary namespaces
using P42Log;
using P42Log.Interfaces;

// Creation of logger instance (with default queue)
P42Logger logger = new P42Logger();

// Adding a log entry
logger.Log("Info", "This is an informational message");

// Setting log level
logger.SetLogLevel("Warn");

// Adding a custom log queue
logger.AddLogQueue(new MyCustomQueue());

In the example:
An instance of P42Logger is created with the default log queue.
A log entry is added with an "Info" log level and a message.
The log level for all log queues is set to "Warn".
A custom log queue (MyCustomQueue) is added to the logger.

Conclusion
P42Logger is a simple yet powerful library that allows you to manage your application's logging
activities effectively. Its extendable design enables you to create custom logging schemes to suit
your needs. Feel free to use and extend it as you see fit!

Note: Make sure to refer to the API documentation for full details on classes,
interfaces, and their methods. This overview may not cover all available methods
or functionality of the library.
