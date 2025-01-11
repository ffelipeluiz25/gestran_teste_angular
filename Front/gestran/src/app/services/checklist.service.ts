import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Checklist } from '../model/checklist.model';
import { LocalStorageService } from './localstorage.service';
import { Router } from '@angular/router';
import { Item } from '../model/item.model';
import { errorContext } from 'rxjs/internal/util/errorContext';

@Injectable({ providedIn: 'root', })

export class ChecklistService {

    constructor(private httpClient: HttpClient, private localStorageService: LocalStorageService, private route: Router) { }

    public listarchecklist(tipoUsuario: number, idUsuarioLogado: number): Observable<any> {
        var token = localStorage.getItem(environment.token);
        const url = `${environment.baseUrlBackend}/checklist/listarchecklist?IdTipoUsuario=${tipoUsuario}&IdUsuarioLogado=${idUsuarioLogado}`;
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.get(url, { headers }).pipe(
            map(this.mapToChecklist),
            catchError(this.handleError),
        )
    }

    public listById(id: number): Observable<Checklist> {
        var token = localStorage.getItem(environment.token);
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        const url = `${environment.baseUrlBackend}/checklist/${id}`
        return this.httpClient.get(url, { headers }).pipe(
            map(this.mapToChecklist)
        )
    }

    private mapToChecklist(data: any): Checklist {
        return data;
    }

    private handleError(error: any): Observable<never> {
        let errorMessage = 'Erro desconhecido!';

        if (error.error instanceof ErrorEvent) {
            errorMessage = `Erro: ${error.error.message}`;
        } else {
            errorMessage = `Código de erro: ${error.status}, Mensagem: ${error.message}`;
            if (error.status === 401) 
                this.route.navigate(['login']);
        }

        return throwError(() => new Error(errorMessage));
    }

    public salvaNovo(checklist: Checklist): Observable<Checklist> {
        var token = localStorage.getItem(environment.token);
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        const url = `${environment.baseUrlBackend}/checklist`
        const parsed = Number(this.localStorageService.getTipoUsuario());
        if (isNaN(parsed))
            return new Observable<Checklist>;

        checklist.idUsuarioAlteracao = Number(this.localStorageService.getTipoUsuario());
        return this.httpClient.post(url, checklist, { headers }).pipe(
            map(this.mapToChecklist)
        )

    }

    public delete(checkId: number): Observable<any> {
        var token = localStorage.getItem(environment.token);
        const url = `${environment.baseUrlBackend}/checklist/${checkId}`;
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.delete(url, { responseType: 'json', headers })
    }

}