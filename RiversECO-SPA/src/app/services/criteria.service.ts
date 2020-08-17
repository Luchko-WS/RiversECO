import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
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

  getUser(id: string): Observable<Criteria> {
    return this.http.get<Criteria>(this.baseUrl + 'criteria/' + id);
  }
}
