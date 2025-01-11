import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { LocalStorageService } from './localstorage.service';

@Injectable({ providedIn: 'root', })

export class LoginService {

    constructor(private httpClient: HttpClient, private localStorageService: LocalStorageService) {

    }

    public login(login: string, senha: string): Observable<any> {
        const url = `${environment.baseUrlBackend}/login`;
        return this.httpClient.post(url, { login, senha }, { responseType: 'json' }).pipe(
            map((data) => {
                this.localStorageService.setTokenLocalStorage(data);
            }),
            catchError((err) => {
                this.localStorageService.removerTokenLocalStorage();
                throw 'Falha ao efetuar login.'
            })
        )
    }


}