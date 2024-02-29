"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/productHub").build();
connection.start();

connection.on("Reload", function () {
    location.reload();
});