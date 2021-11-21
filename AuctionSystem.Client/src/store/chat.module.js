import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
const connection = new HubConnectionBuilder()
    .withUrl('https://localhost:44305/chat')
    .configureLogging(LogLevel.Information)
    .build();

connection.on("ReceiveMessage", function (username, recipient, message) {
    console.log(message);
    var chatBoxes = JSON.parse(localStorage.getItem('chatBoxes'));
    let index = chatBoxes.length == 0 ? -1 : chatBoxes.findIndex((x) => x.recipient == recipient);
    if (index > -1) {
        chatBoxes[index].visible = true;
        chatBoxes[index].collapsed = false;
    }
    else {
        chatBoxes.push(new ChatBox(username, recipient, true, false));
        localStorage.setItem('chatBoxes', JSON.stringify(chatBoxes));
    }
});

import ChatBox from '@/models/chatBox.js';

export const signalr = {
    namespaced: true,
    state: {
        connection: connection
    }
}

connection.start();