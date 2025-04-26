import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  var user=localStorage.getItem('user')
  if(user){
  return true;
  }
  return false;
};
