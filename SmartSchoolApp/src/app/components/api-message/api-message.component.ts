import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-api-message',
  templateUrl: './api-message.component.html',
  styleUrls: ['./api-message.component.css']
})
export class ApiMessageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5000/hub')
    .withAutomaticReconnect()
    .build();

    connection.on('messageReceived', (message: string) => {
      console.log(`${message}`);
    });

    connection.start()
      .then(() => console.log('Connected'))
      .catch(err => console.error(err));
  }

}
