import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Checklist } from '../model/checklist.model';
import { LocalStorageService } from './localstorage.service';
import { Router } from '@angular/router';
import { AssumeExecucaoChecklist } from '../model/assumeexecucaochecklist.model';
import Swal from 'sweetalert2';
import { ChecklistExecutaRequest } from '../model/ChecklistExecutaRequest.model';

@Injectable({ providedIn: 'root', })

export class ChecklistService {

    constructor(private httpClient: HttpClient, private localStorageService: LocalStorageService, private route: Router) { }

    public listarchecklist(tipoUsuario: number, idUsuarioLogado: number): Observable<any> {
        var token = this.localStorageService.getToken();
        const url = `${environment.baseUrlBackend}/checklist/listarchecklist?IdTipoUsuario=${tipoUsuario}&IdUsuarioLogado=${idUsuarioLogado}`;
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.get(url, { headers }).pipe(
            map(this.mapToChecklist),
            catchError(this.handleError),
        )
    }

    public listarPorId(id: number): Observable<Checklist> {
        var token = this.localStorageService.getToken();
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        const url = `${environment.baseUrlBackend}/checklist/listarPorId/${id}`
        return this.httpClient.get(url, { headers }).pipe(
            map(this.mapToChecklist)
        )
    }

    private mapToChecklist(data: any): Checklist {
        return data;
    }

    private mapToDynamic(data: any): any {
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
        var token = this.localStorageService.getToken();
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

    public atualizar(checklist: Checklist): Observable<Checklist> {
        const url = `${environment.baseUrlBackend}/checklist`
        var token = this.localStorageService.getToken();
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        checklist.idUsuarioAlteracao = Number(this.localStorageService.getTipoUsuario());
        return this.httpClient.put(url, checklist, { headers }).pipe(
            map(this.mapToChecklist)
        )
    }

    public executarChecklist(checklist: ChecklistExecutaRequest): Observable<any> {
        const url = `${environment.baseUrlBackend}/checklist/executarchecklist`
        var token = this.localStorageService.getToken();
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.post(url, checklist, { headers }).pipe(
            map(this.mapToDynamic)
        )
    }

    public atualizarStatus(checklist: Checklist): Observable<Checklist> {
        const url = `${environment.baseUrlBackend}/checklist/atualizarstatus`
        var token = this.localStorageService.getToken();
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        checklist.idUsuarioAlteracao = Number(this.localStorageService.getTipoUsuario());
        return this.httpClient.put(url, checklist, { headers }).pipe(
            map(this.mapToChecklist)
        )
    }

    public assumirExecucaoChecklist(idChecklist: number): Observable<any> {
        const url = `${environment.baseUrlBackend}/checklist/assumeexecucaochecklist`
        var token = this.localStorageService.getToken();
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.post(url, new AssumeExecucaoChecklist(idChecklist, Number(this.localStorageService.getTipoUsuario())), { headers }).pipe(
            map(this.mapToDynamic)
        )
    }

    public delete(checkId: number): Observable<any> {
        var token = this.localStorageService.getToken();
        const url = `${environment.baseUrlBackend}/checklist/${checkId}`;
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.delete(url, { responseType: 'json', headers })
    }

}