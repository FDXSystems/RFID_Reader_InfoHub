
// =========================================================================== //
// Instance of client web socket defined within the class or somewhere else -> 
private System.Net.WebSockets.ClientWebSocket ws_ = new ClientWebSocket();
// =========================================================================== //


// The address depends on how the Reader has been configured. By default for
// an Ethernet based connection, it will be: "192.168.1.1"
public void ConnectSocket( string address )
{

    string websocket_address = "ws://" + address + ":" + "/websocket";
    Uri websocketURI = new Uri(websocket_address);
    try
    {
        ws_.ConnectAsync(serverUri, CancellationToken.None).GetAwaiter().GetResult();
        Console.WriteLine("Connected to FDX Reader WS.");

    }
    catch (System.Net.WebSockets.WebSocketException ex)
    {
        Console.WriteLine($"Failed to connect to FDX Reader WS: {ex.Message}");
    }

}

// This function serves to send data to the socket, in a way that it locks
// until the data has been sent
private async Task SendDataAsync(string data) 
{ 
    try 
    { 
        if(ws_.State == WebSocketState.Open) 
        { 
            byte[] buffer = Encoding.UTF8.GetBytes(data); 
            await ws_.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None); 
 
        } 
        else 
        { 
            throw new InvalidOperationException("WebSocket is not in an open state. Cannot send message."); 
        } 
    } 
    catch (Exception ex) 
    { 
        Console.WriteLine($"Error sending data over WebSocket: {ex.Message}"); 
        throw; 
    } 
} 

// This function serves to receive data from the socket.
private async Task<string> ReceiveDataAsync( )
{
    try
    {
        // Define a buffer to store the received data
        byte[] buffer = new byte[20000];

        if(ws_.State == WebSocketState.Open)
        {
            // Receive data asynchronously
            WebSocketReceiveResult result = ws_.ReceiveAsync(new ArraySegment<byte>(buffer), new System.Threading.CancellationToken()).ConfigureAwait(false).GetAwaiter().GetResult();

            //WebSocketReceiveResult result = await ws_.ReceiveAsync(new ArraySegment<byte>(buffer), token);
            // Check if the message type is text
            if (result.MessageType == WebSocketMessageType.Text)
            {
                // Convert the received bytes to a string
                string receivedData = Encoding.UTF8.GetString(buffer, 0, result.Count);
                return receivedData;
            }
            else
            {
                throw new InvalidOperationException("Received message is not of type Text.");
            }
        }
        else
        {
            throw new InvalidOperationException("WebSocket is not in an open state. Cannot receive message.");
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error receiving data over WebSocket: {ex.Message}");
        throw;
    }

}

// Since the reader works in a request-response manner, you can use a function like the following
// to send a request and wait for its response. 
public async Task<string> SendRequestAndWaitForResponse(string request)
{
    try
    {
        var sendTask = SendDataAsync(request);
        var receiveTask = ReceiveDataAsync();
        receiveTask.WaitForResult();
        return receiveTask.Result;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error sending request and waiting for response");
        return null;
    }
}
