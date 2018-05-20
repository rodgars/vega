import { Injectable } from '@angular/core';
import { AuthService } from './auth';

@Injectable()
export class AuthGuard {

    constructor(protected auth: AuthService) { }

    canActivate(){
        if(this.auth.isAuthenticated())
            return true;
        
        return false;
    }
}