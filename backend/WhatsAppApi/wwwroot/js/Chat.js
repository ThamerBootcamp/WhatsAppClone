"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established


connection.on("ReceiveMessage", function (message, receiverId ) {
    //
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

const joinRoom = (room) => 
{
    return connection.invoke("JoinRoom", room)
    
    .catch((err) => {
        return console.error(err.toString());
    })
}

const SendMessage = (jwt , room, receiverId, message) => {

    // add your UI stuff
    connection.invoke("SendMessage", jwt, room, receiverId, message).catch(function (err) {
        return console.error(err.toString());
    });
    
}