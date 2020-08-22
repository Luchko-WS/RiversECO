import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { CriteriaService } from '../services/criteria.service';
import { Criteria } from '../models/criteria';

@Injectable()
export class CriteriasListResolver implements Resolve<Criteria[]> {
    constructor(private criteriaService: CriteriaService, private router: Router) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Criteria[]> {
        return this.criteriaService.getCriterias().pipe(
            catchError(error => {
                console.error(error);
                this.router.navigate(['']);
                return of(null);
            })
        );
    }
}
