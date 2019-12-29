import { Injectable } from '@angular/core';
import { UserManager, User } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';

import { BaseService } from '../services/base.service';
import { ConfigService } from '../services/config.service';

@Injectable({
    providedIn: 'root'
})
export class AuthService extends BaseService {

    // Observable navItem source
    private authNavStatusSource = new BehaviorSubject<boolean>(false);
    // Observable navItem stream
    authNavStatus$ = this.authNavStatusSource.asObservable();

    private manager = new UserManager({
        authority: this.configService.authAppURI,
        client_id: 'angular_spa',
        redirect_uri: 'http://localhost:4200/auth-callback',
        post_logout_redirect_uri: 'http://localhost:4200/',
        response_type: "code",
        scope: "openid profile email api.read",
        filterProtocolClaims: true,
        loadUserInfo: true
    });

    private user: User | null;

    constructor(private configService: ConfigService) {
        super();

        this.manager.getUser().then(user => {
            this.user = user;
            this.authNavStatusSource.next(this.isAuthenticated());
        });
    }

    login(newAccount?: boolean, userName?: string) {

        let extraQueryParams = newAccount && userName ? {
            newAccount: newAccount,
            userName: userName
        } : {};

        // https://github.com/IdentityModel/oidc-client-js/issues/315
        return this.manager.signinRedirect({
            extraQueryParams
        });
    }

    async completeAuthentication() {
        this.user = await this.manager.signinRedirectCallback();
        this.authNavStatusSource.next(this.isAuthenticated());
    }

    isAuthenticated(): boolean {
        return this.user != null && !this.user.expired;
    }

    get authorizationHeaderValue(): string {

        return this.user ? `${this.user.token_type} ${this.user.access_token}` : null;
    }

    get role(): string {
        return this.user != null ? this.user.profile["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] : '';
    }

    get name(): string {
        return this.user != null ? this.user.profile.name : '';
    }

    async signout() {
        await this.manager.signoutRedirect();
    }
}
