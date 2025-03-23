import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { throwError, TimeoutError } from 'rxjs';
import { catchError, timeout } from 'rxjs/operators';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const REQUEST_TIMEOUT = 30000;

  return next(req).pipe(
    timeout(REQUEST_TIMEOUT), 

    catchError((error) => {
      let errorMessage = 'אירעה שגיאה בלתי צפויה';
      if (error.status === 401) {
        errorMessage = 'אינך מחובר, התחבר מחדש'
        alert(errorMessage)
        router.navigate(['login'])
      }
      else if (error.status === 403) {
        errorMessage = 'אין לך הרשאה לצפות במשאב זה'
        alert(errorMessage)
      }
      else if (error.status === 500) {
        errorMessage = 'אירעה שגיאה בלתי צפויה אנא נסה שנית מאוחר יותר'
        alert(errorMessage)
      }

      
      if (error instanceof TimeoutError) {
        errorMessage = '⏳ הבקשה לקחה יותר מדי זמן, אנא נסה שוב';
        alert(errorMessage);
    }
    return throwError(() => new Error(errorMessage));

}
    ))}
