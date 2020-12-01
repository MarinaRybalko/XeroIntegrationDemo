import {Component, OnDestroy, OnInit} from '@angular/core';
import {XeroService} from './services/xero.service';
import {takeWhile} from 'rxjs/operators';
import {ConnectionState} from './models/connection-state.enum';

@Component({
  selector: 'app-connect-to-xero',
  templateUrl: './connect-to-xero.component.html',
  styleUrls: ['./connect-to-xero.component.css']
})
export class ConnectToXeroComponent implements OnInit, OnDestroy {
  isAlive = true;
  isConnected = false;

  constructor(private xeroService: XeroService) { }

  ngOnInit() {
    this.xeroService.getConnection()
      .pipe(takeWhile(() => this.isAlive))
      .subscribe(
        res => this.isConnected = res.state === ConnectionState.Connected
        );
  }

  connect() {
    this.xeroService.connect()
      .pipe(takeWhile(() => this.isAlive))
      .subscribe(res => location.href = res.uri);
  }

  disconnect() {
    this.xeroService.disconnect()
      .pipe(takeWhile(() => this.isAlive))
      .subscribe(
        () => this.isConnected = false,
        err => this.isConnected = true);
  }

  ngOnDestroy() {
    this.isAlive = false;
  }
}
