import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
@Injectable({ providedIn: 'root', })
export class LocalStorageService {

    constructor() { }

    public getToken(): string | null {
        return localStorage.getItem(environment.token);
    }

    public getTipoUsuario(): string | null {
        return localStorage.getItem(environment.tipoUsuario);
    }

    public getIdUsuarioLogado(): string | null {
        return localStorage.getItem(environment.idUsuarioLogado);
    }

    public getNomeUsuario(): string | null {
        return localStorage.getItem(environment.nomeUsuario);
    }

    public setTokenLocalStorage(response: any): void {
        const { idUsuarioLogado, acessToken, tipoUsuario, nomeUsuario } = response;
        localStorage.setItem(environment.token, acessToken);
        localStorage.setItem(environment.tipoUsuario, tipoUsuario);
        localStorage.setItem(environment.idUsuarioLogado, idUsuarioLogado);
        localStorage.setItem(environment.nomeUsuario, nomeUsuario);
    }

    public removerTokenLocalStorage(): void {
        localStorage.removeItem(environment.token);
        localStorage.removeItem(environment.tipoUsuario);
        localStorage.removeItem(environment.idUsuarioLogado);
        localStorage.removeItem(environment.nomeUsuario);
    }
}