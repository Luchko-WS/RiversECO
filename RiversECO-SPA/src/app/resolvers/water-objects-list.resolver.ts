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
                    long: 49,
                    lat: 50,
                    state: 1
                },
                {
                    id: 'id2',
                    name: 'name 2',
                    long: 50,
                    lat: 51,
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
