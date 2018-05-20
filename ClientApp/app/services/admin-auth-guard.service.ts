import { Injectable } from '@angular/core';
import { AuthService } from './auth';
import { AuthGuard } from './auth-guard.service';

@Injectable()
export class AdminAuthGuard extends AuthGuard {

    constructor(auth: AuthService) { 
        super(auth);
    }

    canActivate(){
        var isAuthenticated = super.canActivate();
        if(!isAuthenticated || !this.auth.isInRole('Admin')){
            this.auth.login();
            return false;
        }   
        return true;
    }
}