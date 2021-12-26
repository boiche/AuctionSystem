import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
import moment from 'moment'
const connection = new HubConnectionBuilder()
    .withUrl('https://localhost:44305/chat')
    .configureLogging(LogLevel.Information)
    .build();

connection.on("ReceiveMessage", function (username, recipient, message, datetime) {
    console.log(message);
    var li = document.createElement("li");

    var messageDiv = document.createElement("div");
    messageDiv.textContent = `${username}: ${message}`;
    messageDiv.classList.add("message");

    var datetimeDiv = document.createElement("div");
    datetimeDiv.textContent = `${moment(datetime).format('D.MM.yyyy HH:mm')}`;
    datetimeDiv.classList.add("datetimeHidden");

    if (JSON.parse(localStorage.getItem('user')).username == username) {
        li.classList.add("messageRowRight");
        messageDiv.classList.add("userMessage");

        li.appendChild(datetimeDiv);
        li.appendChild(messageDiv);
    }
    else {
        li.classList.add("messageRowLeft");
        messageDiv.classList.add("receiverMessage");
        
        li.appendChild(messageDiv);
        li.appendChild(datetimeDiv);
    }

    messageDiv.addEventListener("mouseover", function () {
        datetimeDiv.classList.replace("datetimeHidden", "datetime");
    });
    messageDiv.addEventListener("mouseleave", function () {
        datetimeDiv.classList.replace("datetime", "datetimeHidden");
    });

    document.getElementById("chatBoxMessages").appendChild(li);
    document.getElementById("chatbox").scrollTop = document.getElementById("chatbox").scrollHeight;
});

export const signalr = {
    namespaced: true,
    state: {
        connection: connection
    }
}

connection.start();