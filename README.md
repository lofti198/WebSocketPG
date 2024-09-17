# WebSocketPG

This project demonstrates a simple WebSocket interaction between a .NET 8 backend and a raw JavaScript frontend. The server processes a simulated long-running task and sends real-time updates to the client. The project includes error handling where the server can notify the client of a failure, and the client will handle the error and terminate the interaction.

## Project Structure

- **WebSocketPG.sln**: The solution file for Visual Studio.
- **Program.cs**: The backend logic implemented in .NET 8 using WebSockets to communicate with the frontend.
- **index.html**: The frontend client implemented using raw JavaScript to communicate with the WebSocket server.
- **appsettings.json** and **appsettings.Development.json**: Configuration files for logging and allowed hosts.
- **launchSettings.json**: Configuration for launching the project locally.

## How It Works

### Backend (Program.cs)
- The backend exposes a WebSocket endpoint (`/start-task`) that the frontend connects to.
- The server simulates a task that progresses through 5 steps, sending updates to the client in real time.
- If an error occurs during the task (simulated in step 3), the server sends an error message to the client and terminates the task.

### Frontend (index.html)
- The frontend connects to the WebSocket server and listens for updates.
- Task progress is displayed to the user in real time.
- If an error occurs, the frontend logs the error and displays it to the user in red, closing the WebSocket connection.

## Running the Project

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A web browser (for testing the client-side code)

### Steps to Run

1. **Clone the repository** (or create the project structure manually as provided above).

2. **Restore dependencies**:
    ```bash
    dotnet restore
    ```

3. **Run the backend**:
    - Open a terminal and navigate to the project directory.
    - Run the following command:
      ```bash
      dotnet run
      ```

4. **Run the frontend**:
    - Serve the `index.html` file locally using a tool like `http-server` or Visual Studio Code's Live Server extension:
      - Install `http-server`:
        ```bash
        npm install -g http-server
        ```
      - Navigate to the `Client` folder and run:
        ```bash
        http-server
        ```
      - Open your browser and navigate to the provided local URL.

5. **Interact with the WebSocket server**:
    - Click the "Start Task" button on the frontend.
    - The server will send updates as the task progresses.
    - In step 3, the server will simulate an error, which will be displayed on the client-side.

### Example of Interaction
1. User clicks **Start Task**.
2. Server sends task progress messages, e.g., `Step 1 completed`, `Step 2 completed`.
3. On step 3, an error is simulated, and the server sends an error message: `Error: An error occurred in step 3`.
4. The client receives the error, displays it, and terminates the WebSocket connection.

## Configuration

### WebSocket Endpoint
- The WebSocket server is available at:
https://localhost:7048/start-task

markdown
Code kopieren

### Configuration Settings
- **launchSettings.json** is configured to **not launch the browser** by default when the backend runs. You can modify the `applicationUrl` in this file if needed for different ports or settings.

## Customization
You can easily extend this demo to handle additional task types or extend the WebSocket communication for more complex interactions. Simply update the task simulation in `Program.cs` and modify the client-side logic in `index.html` accordingly.

## Troubleshooting

- If you receive WebSocket connection errors, make sure the server is running, and the correct port is used (`https://localhost:7048`).
- Check the browser console (F12) for any client-side errors related to the WebSocket connection.
- Ensure that the SSL settings in `launchSettings.json` are correctly configured to avoid certificate issues when running the backend over HTTPS.

## License
This demo is licensed under the MIT License. Feel free to use and modify it as needed.
