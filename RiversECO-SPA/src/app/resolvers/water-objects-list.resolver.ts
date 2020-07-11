import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { WaterObject } from '../models/water-object';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class WaterObjectsListResolver implements Resolve<WaterObject[]> {
    constructor() { }

    resolve(route: ActivatedRouteSnapshot): Observable<WaterObject[]> {
        const observer = of<WaterObject[]>(
            [
                {
                    id: 'id1',
                    name: 'name 1',
                    state: 1
                },
                {
                    id: 'id2',
                    name: 'name 2',
                    state: 2
                }
            ]
        );

        return observer.pipe(
            catchError(error => {
                return of(null);
            })
        );
    }
}
