import { throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

export abstract class BaseService {  
    
    constructor() { }

    protected handleError(error: HttpErrorResponse) {

      var applicationError = error.headers.get('Application-Error');

      // 1. check for application error in header or model error in body
      if (applicationError) {
         return throwError(applicationError);
      }

      // 2. check errorEvent = problems contacting the API should land in here     
      if (error.error instanceof ErrorEvent) {
         // A client-side or network error occurred. Handle it accordingly.
         console.error('An error occurred:', error.error.message);
      } else {
      
        if(error.status == 0){
          return throwError(error.statusText);
        }

        // 3. check for modelstate issues
        var modelStateErrors: string = '';      

        // for now just concatenate the error descriptions, alternative we could simply pass the entire error response downstream
        for (var key in error.error) {        
          if (error.error[key]) modelStateErrors += error.error[key].description + '\n'; 
        }
      
      modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
      return throwError(modelStateErrors || 'Server error'); 
      }
    
      // return an observable with a user-facing error message
      return throwError('Request failed, please try again!'); 
  }
}