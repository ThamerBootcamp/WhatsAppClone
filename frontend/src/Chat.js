const signalR = require("@microsoft/signalr");

var connection

export function connect(){
    connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44388/chatHub").build();
    connection.start()
}

export function onMessage(callback){
    connection.on("ReceiveMessage", callback);
}

export const joinRoom = (room) => 
{
    return connection.invoke("JoinRoom", room)
    
    .catch((err) => {
        return console.error(err.toString());
    })
}

export const SendMessage = (jwt , room, receiverId, message) => {

    // add your UI stuff
    connection.invoke("SendMessage", jwt, room, receiverId, message).catch(function (err) {
        return console.error(err.toString());
    });
    
}