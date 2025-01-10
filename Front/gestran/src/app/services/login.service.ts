import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root', })

export class LoginService {

    constructor(private httpClient: HttpClient) {

    }

    public login(login: string, senha: string): Observable<any> {
        const url = `${environment.baseUrlBackend}/login`;
        return this.httpClient.post(url, { login, senha }, { responseType: 'json' }).pipe(
            map((data) => {
                this.setTokenLocalStorage(data);
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

    public getTipoUsuario(): string | null {
        return localStorage.getItem(environment.tipoUsuario);
    }

    private setTokenLocalStorage(response: any): void {
        const { acessToken, tipoUsuario } = response;
        localStorage.setItem(environment.token, acessToken);
        localStorage.setItem(environment.tipoUsuario, tipoUsuario);
    }

    public removerTokenLocalStorage(): void {
        localStorage.removeItem(environment.token);
        localStorage.removeItem(environment.tipoUsuario);
    }
}