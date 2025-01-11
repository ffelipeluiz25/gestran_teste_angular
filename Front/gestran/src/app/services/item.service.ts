import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { LocalStorageService } from './localstorage.service';
import { Router } from '@angular/router';
import { Item } from '../model/item.model';

@Injectable({ providedIn: 'root', })

export class ItemService {

    constructor(private httpClient: HttpClient, private localStorageService: LocalStorageService, private route: Router) { }

    public listarTodos(): Observable<any> {
        var token = localStorage.getItem(environment.token);
        const url = `${environment.baseUrlBackend}/Item/listartodos`;
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.get(url, { headers }).pipe(
            map(this.mapToItem),
            catchError(this.handleError)
        )
    }

    public listById(id: number): Observable<Item> {
        var token = localStorage.getItem(environment.token);
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        const url = `${environment.baseUrlBackend}/Item/${id}`
        return this.httpClient.get(url, { headers }).pipe(
            map(this.mapToItem)
        )
    }

    private mapToItem(data: any): Item {
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

    public salvaNovo(Item: Item): Observable<Item> {
        var token = localStorage.getItem(environment.token);
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        const url = `${environment.baseUrlBackend}/Item`
        const parsed = Number(this.localStorageService.getTipoUsuario());
        if (isNaN(parsed))
            return new Observable<Item>;

        Item.idUsuarioAlteracao = Number(this.localStorageService.getTipoUsuario());
        return this.httpClient.post(url, Item, { headers }).pipe(
            map(this.mapToItem)
        )

    }

    public delete(checkId: number): Observable<any> {
        var token = localStorage.getItem(environment.token);
        const url = `${environment.baseUrlBackend}/Item/${checkId}`;
        const headers = new HttpHeaders({
            Authorization: `Bearer ${token}`,
        });
        return this.httpClient.delete(url, { responseType: 'json', headers })
    }

}