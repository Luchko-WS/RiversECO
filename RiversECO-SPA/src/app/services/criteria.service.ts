import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Criteria } from '../models/criteria';

@Injectable({
  providedIn: 'root'
})

export class CriteriaService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCriterias(): Observable<Criteria[]> {
    return this.http.get<Criteria[]>(this.baseUrl + 'criteria');
  }

  getCriteria(id: string): Observable<Criteria> {
    return this.http.get<Criteria>(this.baseUrl + 'criteria/' + id);
  }

  createCriteria(criteria: Criteria): Observable<Criteria> {
    return this.http.post<Criteria>(this.baseUrl + 'criteria/', criteria);
  }

  updateCriteria(criteria: Criteria): Observable<Criteria> {
    return this.http.put<Criteria>(this.baseUrl + 'criteria/', criteria);
  }

  deleteCriteria(id: string) {
    return this.http.delete(this.baseUrl + 'criteria/' + id);
  }

  deleteCriterias(ids: string[]) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: ids,
    };

    return this.http.delete(this.baseUrl + 'criteria/', options);
  }
}
