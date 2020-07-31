import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Criteria } from '../models/criteria';

@Injectable({
  providedIn: 'root'
})

export class CriteriaService {
  constructor(private http: HttpClient) { }

  getCriterias(): Observable<Criteria[]> {
    return of<Criteria[]>([
        {
            name: 'Criteria 1',
            description: 'Criteria 1',
        },
        {
            name: 'Criteria 2',
            description: 'Criteria 2',
        },
        {
            name: 'Criteria 3',
            description: 'Criteria 3',
        },
        {
            name: 'Criteria 4',
            description: 'Criteria 4',
        }
    ]);
  }
}
