import { Injectable } from '@angular/core';

@Injectable()
export class ConfigService {

    static readonly RESOURCE_BASE_URI = "https://localhost:5025";
    static readonly AUTH_BASE_URI = "https://localhost:8787";

    constructor() { }

    getDefaultRoute(role: string): string {

        switch (role) {
            case 'employer': return '/employer/jobs';
            case 'applicant': return '/jobs/list';
        }
    }

    get authApiURI() {
        return ConfigService.AUTH_BASE_URI + '/api';
    }

    get authAppURI() {
        return ConfigService.AUTH_BASE_URI;
    }

    get resourceApiURI() {
        return ConfigService.RESOURCE_BASE_URI + "/api";
    }

    get graphqlURI() {
        return ConfigService.RESOURCE_BASE_URI + "/graphql";
    }

}