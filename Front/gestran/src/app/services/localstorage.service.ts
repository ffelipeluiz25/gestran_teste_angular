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

    public setTokenLocalStorage(response: any): void {
        const { idUsuarioLogado, acessToken, tipoUsuario } = response;
        localStorage.setItem(environment.token, acessToken);
        localStorage.setItem(environment.tipoUsuario, tipoUsuario);
        localStorage.setItem(environment.idUsuarioLogado, idUsuarioLogado);
    }

    public removerTokenLocalStorage(): void {
        localStorage.removeItem(environment.token);
        localStorage.removeItem(environment.tipoUsuario);
        localStorage.removeItem(environment.idUsuarioLogado);
    }
}