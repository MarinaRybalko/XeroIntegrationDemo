import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Connection} from '../models/connection.model';
import {LoginUri} from '../models/login-uri.model';

@Injectable({providedIn: 'root'})
export class XeroService {

  constructor(private client: HttpClient) {}

  connect(): Observable<LoginUri> {
    return this.client.get<LoginUri>('api/xero/auth/login-uri');
  }

  disconnect(): Observable<void> {
    return this.client.post<void>('api/xero/auth/disconnect', {});
  }

  getConnection(): Observable<Connection> {
    return this.client.get<Connection>('api/xero/auth/connection');
  }
}
