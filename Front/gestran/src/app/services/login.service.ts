import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root',
})


export class LoginService {

    constructor(private httpClient: HttpClient) {

    }

    public login(username: string, password: string): Observable<any> {
        const url = `${environment.baseUrlBackend}/weatherforecast`;
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*'
        });
        return this.httpClient.post(url, { username, password }, { responseType: 'json', headers: headers }).pipe(
            map((data) => {
                debugger;
                //this.setTokenLocalStorage(data);
                this.setTokenLocalStorage({ type: 'json', token: 'dasdsadsadsadsadadsadasdadaad5asda5s61das541d5a4sd8as1das1dsa1sadda' });
            }),
            catchError((err) => {
                this.removerTokenLocalStorage();
                throw 'Falha ao efetuar login.'
            })
        )
    }

    public getToken(): string | null {
        return localStorage.getItem(environment.token);
    }

    private setTokenLocalStorage(response: any): void {
        const { type, token, _ } = response;
        localStorage.setItem(environment.token, token)
    }

    private removerTokenLocalStorage(): void {
        localStorage.removeItem(environment.token);
    }
}