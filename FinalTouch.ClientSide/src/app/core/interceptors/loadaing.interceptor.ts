import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs';
import { BusyService } from '../services/busy.service';

export const loadaingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService)

  busyService.busy();
  return next(req).pipe(
    delay(300),
    finalize(()=> busyService.idle()),
  )
};
