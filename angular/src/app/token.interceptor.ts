import { HttpInterceptorFn } from '@angular/common/http';
import { UserService } from './service/user.service';
import { inject } from '@angular/core';


export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(UserService)
  const isRequestAuthorized = authService.isAuthenticated$ && req.url.startsWith('https://localhost:7194')
  let token: string | null = null
  if (typeof window !== 'undefined' && window.localStorage) {
    try {
      const storedSession = localStorage.getItem('user');
      token = storedSession ? JSON.parse(storedSession)?.token : null;
    } catch (error) {
      console.error('Error parsing token from localStorage:', error);
    }
  }
  if (isRequestAuthorized && token) {
    const cloneRequest = req.clone({
      
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
    return next(cloneRequest);
  }

  return next(req);
};