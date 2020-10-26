import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ApiNotification } from 'src/app/models/ApiNotification';

@Component({
  selector: 'app-api-message',
  templateUrl: './api-message.component.html',
  styleUrls: ['./api-message.component.css']
})
export class ApiMessageComponent implements OnInit {

  public messages = Array<ApiNotification>();

  constructor() { }

  ngOnInit(): void {
    const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5000/hub')
    .withAutomaticReconnect()
    .build();

    connection.on('messageReceived', (call: string, message: string) => {
      this.messages.push({ call, message });
    });

    connection.start()
      .then(() => {
        const call = '-';
        const message = 'Connected';

        this.messages.push({ call, message });
      })
      .catch(err => console.error(err));
  }
}
