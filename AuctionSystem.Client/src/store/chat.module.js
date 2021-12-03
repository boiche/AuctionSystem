import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
const connection = new HubConnectionBuilder()
    .withUrl('https://localhost:44305/chat')
    .configureLogging(LogLevel.Information)
    .build();

connection.on("ReceiveMessage", function (username, recipient, message) {
    console.log(message);
    var li = document.createElement("li");
    li.textContent = `${username}: ${message}`;
    document.getElementById("chatBoxMessages").appendChild(li);
});

export const signalr = {
    namespaced: true,
    state: {
        connection: connection
    }
}

connection.start();