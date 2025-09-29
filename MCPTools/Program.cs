using MCPTools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddSingleton<MCPServices>();

await builder.Build().RunAsync();

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"hello from program{message}";

    [McpServerTool, Description("Echoes inform about Adds two integers.")]
    public static int Add(int a, int b) => a + b;

    [McpServerTool, Description("Echoes inform about Multiplies two doubles.")]
    public static double Multiply(double x, double y) => x * y; 

    [McpServerTool, Description("Echoes inform about Reverse a string.")]
    public static string Reverse(string input) => new string(input.Reverse().ToArray());
}
