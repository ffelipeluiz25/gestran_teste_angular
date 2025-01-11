import { CanActivate, Router, UrlTree } from '@angular/router';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { LocalStorageService } from '../services/localstorage.service';
@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(private route: Router, private localStorageService: LocalStorageService) { }

    canActivate(): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        const token = this.localStorageService.getToken();
        if (token === null || token === "null" || token === undefined || token === "undefined") {
            this.route.navigate(['login']);
            return false;
        }
        return true;
    }

}