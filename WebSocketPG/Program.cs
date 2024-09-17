using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

app.Map("/start-task", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await StartLongRunningTask(webSocket);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

async Task StartLongRunningTask(WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    var taskFailed = false; // Variable to simulate task failure

    // Start processing the task with multiple steps
    for (int i = 1; i <= 5 && !taskFailed; i++)
    {
        // Simulating an error in step 3
        if (i == 3)
        {
            var errorMessage = JsonSerializer.Serialize(new { type = "error", message = "An error occurred in step 3" });
            var errorBytes = Encoding.UTF8.GetBytes(errorMessage);
            await webSocket.SendAsync(new ArraySegment<byte>(errorBytes, 0, errorBytes.Length), WebSocketMessageType.Text, true, CancellationToken.None);

            taskFailed = true; // Simulating task failure
            break; // Exit the task loop on error
        }

        // Send task progress message
        var message = Encoding.UTF8.GetBytes($"Step {i} completed");
        await webSocket.SendAsync(new ArraySegment<byte>(message, 0, message.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        await Task.Delay(1000); // Simulate some delay for each step
    }

    if (!taskFailed)
    {
        var finalMessage = Encoding.UTF8.GetBytes("Task completed");
        await webSocket.SendAsync(new ArraySegment<byte>(finalMessage, 0, finalMessage.Length), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Task finished", CancellationToken.None);
}

app.Run();
