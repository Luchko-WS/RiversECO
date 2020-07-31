import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { WaterObjectService } from '../services/water-object.service';
import { WaterObject } from '../models/water-object';

@Injectable()
export class WaterObjectsListResolver implements Resolve<WaterObject[]> {
    constructor(private waterObjectService: WaterObjectService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<WaterObject[]> {
        return this.waterObjectService.getWaterObjects().pipe(
            catchError(error => {
                console.error(error);
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
