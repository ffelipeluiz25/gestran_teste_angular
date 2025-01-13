import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { ChecklistItem } from '../model/checklistItem.model';
import { LocalStorageService } from './localstorage.service';

@Injectable({ providedIn: 'root', })

export class ChecklistItemService {

    constructor(private httpClient: HttpClient, private route: Router, private localStorageService: LocalStorageService) { }

    public listarPorIdChecklist(idChecklist: number): Observable<any> {
        var token = this.localStorageService.getToken();
        const url = `${environment.baseUrlBackend}/checklistitem/listarporidchecklist/${idChecklist}`;
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.get(url, { headers }).pipe(
            map(this.mapToChecklistItem),
            catchError(this.handleError),
        )
    }

    private mapToChecklistItem(data: any): ChecklistItem {
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

}